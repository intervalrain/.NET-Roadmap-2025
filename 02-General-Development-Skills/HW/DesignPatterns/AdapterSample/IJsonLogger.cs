namespace DesignPatterns.AdapterSample;

// Target interface expected by new code
public interface IJsonLogger
{
    void LogJson(string jsonData);
}
