using DnsDemo.Interfaces;
using Microsoft.Extensions.Logging;

namespace DnsDemo.Services;

public class RootServer(ILogger<RootServer> logger) : IRootServer
{
    private readonly Dictionary<string, string> _tldServers = new()
    {
        { "com", "com-tld-server.net" },
        { "org", "org-tld-server.net" },
        { "net", "net-tld-server.net" }
    };

    public async Task<string?> GetTldServerAsync(string domain)
    {
        await Task.Delay(50);
        
        var tld = domain.Split('.').LastOrDefault();
        if (tld != null && _tldServers.TryGetValue(tld, out var tldServer))
        {
            logger.LogInformation("[Root Server] Found TLD server for .{Tld}: {TldServer}", tld, tldServer);
            return tldServer;
        }
        
        logger.LogInformation("[Root Server] No TLD server found for domain {Domain}", domain);
        return null;
    }
}