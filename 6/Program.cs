using System.Xml;

public class Figure
{
    public string Name { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Figure(string name, int width, int height)
    {
        Name = name;
        Width = width;
        Height = height;
    }
}
public class FileManager
{
    public Figure LoadFile(string filePath)
    {
        string extension = Path.GetExtension(filePath);

        switch (extension.ToLower())
        {
            case ".txt":
                return LoadTxtFile(filePath);
            case ".json":
                return LoadJsonFile(filePath);
            case ".xml":
                return LoadXmlFile(filePath);
            default:
                throw new ArgumentException("Unsupported file format");
        }
    }

    private Figure LoadTxtFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);

        // Парсинг строк файла и создание объекта Figure
        string name = lines[0];
        int width = int.Parse(lines[1]);
        int height = int.Parse(lines[2]);

        return new Figure(name, width, height);
    }

    private Figure LoadJsonFile(string filePath)
    {
        string json = File.ReadAllText(filePath);

        // Десериализация JSON и создание объекта Figure

        return JsonConvert.DeserializeObject<Figure>(json);
    }

    private Figure LoadXmlFile(string filePath)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filePath);

        // Чтение XML и создание объекта Figure
        XmlNode root = doc.DocumentElement;
        string name = root.SelectSingleNode("Name").InnerText;
        int width = int.Parse(root.SelectSingleNode("Width").InnerText);
        int height = int.Parse(root.SelectSingleNode("Height").InnerText);

        return new Figure(name, width, height);
    }

    public void SaveFile(Figure figure, string filePath)
    {
        string extension = Path.GetExtension(filePath);

        switch (extension.ToLower())
        {
            case ".txt":
                SaveTxtFile(figure, filePath);
                break;
            case ".json":
                SaveJsonFile(figure, filePath);
                break;
            case ".xml":
                SaveXmlFile(figure, filePath);
                break;
            default:
                throw new ArgumentException("Unsupported file format");
        }
    }

    private void SaveTxtFile(Figure figure, string filePath)
    {
        string[] lines = { figure.Name, figure.Width.ToString(), figure.Height.ToString() };
        File.WriteAllLines(filePath, lines);
    }

    private void SaveJsonFile(Figure figure, string filePath)
    {
        string json = JsonConvert.SerializeObject(figure);
        File.WriteAllText(filePath, json);
    }

    private void SaveXmlFile(Figure figure, string filePath)
    {
        XmlDocument doc = new XmlDocument();

        XmlElement rootElement = doc.CreateElement("Figure");
        doc.AppendChild(rootElement);

        XmlElement nameElement = doc.CreateElement("Name");
        nameElement.InnerText = figure.Name;

        XmlElement widthElement = doc.CreateElement("Width");
        widthElement.InnerText = figure.Width.ToString();

        XmlElement heightElement = doc.CreateElement("Height");
        heightElement.InnerText = figure.Height.ToString();

        rootElement.AppendChild(nameElement);
        rootElement.AppendChild(widthElement);
        rootElement.AppendChild(heightElement);

        doc.Save(filePath);
    }
}

internal class JsonConvert
{
    internal static T DeserializeObject<T>(string json)
    {
        throw new NotImplementedException();
    }

    internal static string SerializeObject(Figure figure)
    {
        throw new NotImplementedException();
    }
}
public class TextEditor
{
    private FileManager fileManager;
    private Figure currentFigure;
    private string currentFilePath;

    public TextEditor()
    {
        fileManager = new FileManager();
    }

    public void Run()
    {
        InitializeFilePath();

        ConsoleKeyInfo key;
        do
        {
            //обработка событий клавиши
            key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.F1:
                    SaveFile();
                    break;
                case ConsoleKey.Escape:
                    break;
            }
        } while (key.Key != ConsoleKey.Escape);
    }

    private void InitializeFilePath()
    {
        Console.WriteLine("Enter file path:");
        currentFilePath = Console.ReadLine();

        currentFigure = fileManager.LoadFile(currentFilePath);
    }

    private void SaveFile()
    {
        fileManager.SaveFile(currentFigure, currentFilePath);
    }
}



