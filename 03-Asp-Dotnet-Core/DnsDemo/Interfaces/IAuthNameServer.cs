namespace DnsDemo.Interfaces;

public interface IAuthNameServer
{
    Task<string?> GetIpAddressAsync(string domain);
}