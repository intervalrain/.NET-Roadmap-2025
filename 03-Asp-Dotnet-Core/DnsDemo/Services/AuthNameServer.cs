using DnsDemo.Interfaces;
using Microsoft.Extensions.Logging;

namespace DnsDemo.Services;

public class AuthNameServer(ILogger<AuthNameServer> logger) : IAuthNameServer
{
    private readonly Dictionary<string, string> _records = new()
    {
        { "www.calculator.com", "192.168.1.100" },
        { "calculator.com", "192.168.1.100" },
        { "example.com", "93.184.216.34" },
        { "google.com", "142.250.191.14" }
    };

    public async Task<string?> GetIpAddressAsync(string domain)
    {
        await Task.Delay(20);
        
        if (_records.TryGetValue(domain, out var ipAddress))
        {
            logger.LogInformation("[Authoritative Name Server] Resolved {Domain} to {IpAddress}", domain, ipAddress);
            return ipAddress;
        }
        
        logger.LogInformation("[Authoritative Name Server] No A record found for {Domain}", domain);
        return null;
    }
}