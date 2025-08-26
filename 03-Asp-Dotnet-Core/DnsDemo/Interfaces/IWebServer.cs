namespace DnsDemo.Interfaces;

public interface IWebServer
{
    Task<string> HandleRequestAsync(string url, string ipAddress);
}