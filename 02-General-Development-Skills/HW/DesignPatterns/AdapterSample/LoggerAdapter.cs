namespace DesignPatterns.AdapterSample;

// Adapter that lets XmlLogger be used where IJsonLogger is expected
public class LoggerAdapter : IJsonLogger
{
    private readonly XmlLogger _xmlLogger;

    public LoggerAdapter(XmlLogger xmlLogger)
    {
        _xmlLogger = xmlLogger ?? throw new System.ArgumentNullException(nameof(xmlLogger));
    }

    public void LogJson(string jsonData)
    {
        // Very simple conversion for demo purposes: wrap JSON in a XML element
        var converted = System.Text.Json.JsonDocument.Parse(jsonData).RootElement.GetRawText();
        _xmlLogger.LogXml(converted);
    }
}
