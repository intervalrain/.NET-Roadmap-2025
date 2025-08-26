namespace DnsDemo.Interfaces;

public interface ITldServer
{
    Task<string?> GetAuthoritativeServerAsync(string domain);
}