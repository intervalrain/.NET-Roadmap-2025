namespace DesignPatterns.AdapterSample;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("AdapterSample demo:");

        // Adaptee produces XML logs
        var xmlLogger = new XmlLogger();
        xmlLogger.LogXml("This comes from the old XML logger");

        // The new system expects IJsonLogger
        IJsonLogger jsonLogger = new LoggerAdapter(xmlLogger);
        jsonLogger.LogJson("{ \"message\": \"This is adapted to JSON\" }");
    }
}
