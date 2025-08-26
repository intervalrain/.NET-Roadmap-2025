namespace DnsDemo.Interfaces;

public interface IRootServer
{
    Task<string?> GetTldServerAsync(string domain);
}