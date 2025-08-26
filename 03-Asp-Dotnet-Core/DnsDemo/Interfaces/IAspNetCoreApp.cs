namespace DnsDemo.Interfaces;

public interface IAspNetCoreApp
{
    Task<string> ProcessRequestAsync(string path, Dictionary<string, string> queryParams);
}