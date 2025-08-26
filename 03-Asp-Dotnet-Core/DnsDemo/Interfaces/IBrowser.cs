namespace DnsDemo.Interfaces;

public interface IBrowser
{
    Task<string> RequestAsync(string url);
}