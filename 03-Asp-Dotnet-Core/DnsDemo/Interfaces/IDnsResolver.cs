namespace DnsDemo.Interfaces;

public interface IDnsResolver
{
    Task<string> ResolveAsync(string domain);
}