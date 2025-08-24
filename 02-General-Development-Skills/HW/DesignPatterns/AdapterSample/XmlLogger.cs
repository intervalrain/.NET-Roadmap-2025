namespace DesignPatterns.AdapterSample;

// Old, incompatible logger (Adaptee)
public class XmlLogger
{
    public void LogXml(string data)
    {
        Console.WriteLine($"<log>{data}</log>");
    }
}
