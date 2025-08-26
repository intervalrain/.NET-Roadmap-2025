using DnsDemo.Interfaces;
using Microsoft.Extensions.Logging;

namespace DnsDemo.Services;

public class TldServerFactory(ILogger<TldServerFactory> logger) : ITldServerFactory
{
    public ITldServer CreateTldServer(string serverAddress)
    {
        logger.LogInformation("[TLD Server Factory] Creating TLD server instance for {ServerAddress}", serverAddress);
        
        return serverAddress switch
        {
            "tld.com" => new ComTldServer(logger),
            "tld.org" => new OrgTldServer(logger),
            _ => new ComTldServer(logger) // Default to .com TLD server
        };
    }
}

public class ComTldServer(ILogger logger) : ITldServer
{
    private readonly Dictionary<string, string> _authServers = new()
    {
        { "www.calculator.com", "ns1.calculator.com" },
        { "calculator.com", "ns1.calculator.com" },
        { "example.com", "ns1.example.com" },
        { "google.com", "ns1.google.com" }
    };

    public async Task<string?> GetAuthoritativeServerAsync(string domain)
    {
        await Task.Delay(30);
        
        logger.LogInformation("[COM TLD Server] Processing query for {Domain}", domain);
        
        if (_authServers.TryGetValue(domain, out var authServer))
        {
            logger.LogInformation("[COM TLD Server] Found authoritative server for {Domain}: {AuthServer}", domain, authServer);
            return authServer;
        }
        
        logger.LogInformation("[COM TLD Server] No authoritative server found for domain {Domain}", domain);
        return null;
    }
}

public class OrgTldServer(ILogger logger) : ITldServer
{
    private readonly Dictionary<string, string> _authServers = new()
    {
        { "wikipedia.org", "ns1.wikipedia.org" },
        { "mozilla.org", "ns1.mozilla.org" }
    };

    public async Task<string?> GetAuthoritativeServerAsync(string domain)
    {
        await Task.Delay(30);
        
        logger.LogInformation("[ORG TLD Server] Processing query for {Domain}", domain);
        
        if (_authServers.TryGetValue(domain, out var authServer))
        {
            logger.LogInformation("[ORG TLD Server] Found authoritative server for {Domain}: {AuthServer}", domain, authServer);
            return authServer;
        }
        
        logger.LogInformation("[ORG TLD Server] No authoritative server found for domain {Domain}", domain);
        return null;
    }
}