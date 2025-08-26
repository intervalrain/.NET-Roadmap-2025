using DnsDemo.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace DnsDemo.Services;

public class Browser(IDnsResolver dnsResolver, IWebServer webServer, ILogger<Browser> logger) : IBrowser
{
    private readonly Dictionary<string, string> _dnsCache = [];

    public async Task<string> RequestAsync(string url)
    {
        logger.LogInformation("[Browser] Starting request for {Url}", url);
        
        var uri = new Uri(url);
        var domain = uri.Host;

        string ipAddress;
        if (_dnsCache.TryGetValue(domain, out var cachedIp))
        {
            ipAddress = cachedIp;
            logger.LogInformation("[Browser] Found {Domain} -> {IpAddress} in DNS cache", domain, ipAddress);
        }
        else
        {
            logger.LogInformation("[Browser] DNS cache miss for {Domain}, resolving...", domain);
            ipAddress = await dnsResolver.ResolveAsync(domain);
            _dnsCache[domain] = ipAddress;
            logger.LogInformation("[Browser] Cached {Domain} -> {IpAddress}", domain, ipAddress);
        }

        var fullUrl = $"{uri.Scheme}://{domain}{uri.PathAndQuery}";
        var htmlContent = await webServer.HandleRequestAsync(fullUrl, ipAddress);
        
        logger.LogInformation("[Browser] Rendering HTML content...");
        await SimulateRendering();
        RenderHtml(htmlContent);
        
        return htmlContent;
    }
    
    private async Task SimulateRendering()
    {
        logger.LogInformation("[Browser] Parsing HTML...");
        await Task.Delay(30);
        logger.LogInformation("[Browser] Building DOM tree...");
        await Task.Delay(20);
        logger.LogInformation("[Browser] Applying CSS styles...");
        await Task.Delay(25);
        logger.LogInformation("[Browser] Layout and painting...");
        await Task.Delay(40);
    }
    
    private void RenderHtml(string htmlContent)
    {
        Console.WriteLine("\n" + new string('=', 80));
        Console.WriteLine("BROWSER RENDERED PAGE");
        Console.WriteLine(new string('=', 80));
        
        var title = ExtractTitle(htmlContent);
        if (!string.IsNullOrEmpty(title))
        {
            Console.WriteLine($"Title: {title}");
            Console.WriteLine(new string('-', 80));
        }
        
        var textContent = ExtractTextContent(htmlContent);
        Console.WriteLine(textContent);
        
        Console.WriteLine(new string('=', 80));
        Console.WriteLine();
    }
    
    private string ExtractTitle(string html)
    {
        var titleMatch = Regex.Match(html, @"<title>(.*?)</title>", RegexOptions.IgnoreCase);
        return titleMatch.Success ? titleMatch.Groups[1].Value.Trim() : "";
    }
    
    private string ExtractTextContent(string html)
    {
        var text = html;
        
        text = Regex.Replace(text, @"<style[^>]*>.*?</style>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        text = Regex.Replace(text, @"<script[^>]*>.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        
        text = Regex.Replace(text, @"<h1[^>]*>(.*?)</h1>", "\n** $1 **\n", RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"<h[2-6][^>]*>(.*?)</h[2-6]>", "\n* $1 *\n", RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"<div[^>]*class='result'[^>]*>(.*?)</div>", "\n>>> RESULT: $1 <<<\n", RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"<div[^>]*class='calculation'[^>]*>(.*?)</div>", "\nCalculation: $1\n", RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"<div[^>]*class='error'[^>]*>(.*?)</div>", "\n!!! ERROR: $1 !!!\n", RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"<p[^>]*>(.*?)</p>", "$1\n", RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"<li[^>]*>(.*?)</li>", "  • $1\n", RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"<strong[^>]*>(.*?)</strong>", "**$1**", RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"<hr[^>]*>", "\n" + new string('-', 60) + "\n", RegexOptions.IgnoreCase);
        
        text = Regex.Replace(text, @"<[^>]+>", "", RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"\s+", " ");
        text = Regex.Replace(text, @"\n\s*\n", "\n");
        
        return text.Trim();
    }
}