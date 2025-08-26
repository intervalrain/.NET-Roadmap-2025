using DnsDemo.Interfaces;

using Microsoft.Extensions.Logging;

namespace DnsDemo.Services;

public class TldServer : ITldServer
{
    public TldServer(ILogger<TldServer> logger)
    {
        _logger = logger;
    }

    private readonly Dictionary<string, string> _authServers = new()
    {
        { "www.calculator.com", "ns1.calculator.com" },
        { "calculator.com", "ns1.calculator.com" },
        { "example.com", "ns1.example.com" },
        { "google.com", "ns1.google.com" }
    };
    private readonly ILogger<TldServer> _logger;


    public async Task<string?> GetAuthoritativeServerAsync(string domain)
    {
        await Task.Delay(30);
        
        if (_authServers.TryGetValue(domain, out var authServer))
        {
            _logger.LogInformation($"TLD Server: Found authoritative server for {domain}: {authServer}");
            return authServer;
        }
        
        _logger.LogInformation($"TLD Server: No authoritative server found for domain {domain}");
        return null;
    }
}