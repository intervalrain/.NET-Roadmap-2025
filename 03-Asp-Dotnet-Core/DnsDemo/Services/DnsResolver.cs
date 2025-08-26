using DnsDemo.Interfaces;
using Microsoft.Extensions.Logging;

namespace DnsDemo.Services;

public class DnsResolver(IRootServer rootServer, ITldServerFactory tldServerFactory, IAuthNameServer authNameServer, ILogger<DnsResolver> logger) : IDnsResolver
{
    public async Task<string> ResolveAsync(string domain)
    {
        logger.LogInformation("[DNS Resolver] Starting resolution for {Domain}", domain);
        
        logger.LogInformation("[DNS Resolver] Querying root server...");
        var tldServerAddress = await rootServer.GetTldServerAsync(domain);
        if (tldServerAddress == null)
        {
            throw new InvalidOperationException($"Root server could not find TLD for {domain}");
        }
        logger.LogInformation("[DNS Resolver] Root server returned TLD server: {TldServerAddress}", tldServerAddress);
        
        logger.LogInformation("[DNS Resolver] Creating TLD server instance for {TldServerAddress}...", tldServerAddress);
        var tldServerInstance = tldServerFactory.CreateTldServer(tldServerAddress);
        
        logger.LogInformation("[DNS Resolver] Querying TLD server...");
        var authServerAddress = await tldServerInstance.GetAuthoritativeServerAsync(domain);
        if (authServerAddress == null)
        {
            throw new InvalidOperationException($"TLD server could not find authoritative server for {domain}");
        }
        logger.LogInformation("[DNS Resolver] TLD server returned authoritative server: {AuthServerAddress}", authServerAddress);
        
        logger.LogInformation("[DNS Resolver] Querying authoritative name server...");
        var ipAddress = await authNameServer.GetIpAddressAsync(domain);
        if (ipAddress == null)
        {
            throw new InvalidOperationException($"Authoritative server could not resolve {domain}");
        }
        logger.LogInformation("[DNS Resolver] Resolved {Domain} to {IpAddress}", domain, ipAddress);
        
        return ipAddress;
    }
}