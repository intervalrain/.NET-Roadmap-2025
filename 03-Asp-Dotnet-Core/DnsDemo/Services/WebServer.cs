using DnsDemo.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using System.Web;

namespace DnsDemo.Services;

public class WebServer(IAspNetCoreApp aspNetCoreApp, ILogger<WebServer> logger) : IWebServer
{
    public async Task<string> HandleRequestAsync(string url, string ipAddress)
    {
        var uri = new Uri(url);
        var isHttps = uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase);
        
        logger.LogInformation("[Web Server] Received request for {Url} at IP {IpAddress}", url, ipAddress);
        
        if (isHttps)
        {
            logger.LogInformation("[Web Server] Establishing TLS handshake...");
            await SimulateTlsHandshake();
        }
        else
        {
            logger.LogInformation("[Web Server] Establishing TCP connection...");
            await SimulateTcpConnection();
        }
        
        logger.LogInformation("[Web Server] Forwarding request to ASP.NET Core application...");
        
        var path = uri.AbsolutePath;
        var queryParams = ParseQueryString(uri.Query);
        
        var response = await aspNetCoreApp.ProcessRequestAsync(path, queryParams);
        
        logger.LogInformation("[Web Server] Sending response back to browser");
        return response;
    }
    
    private async Task SimulateTcpConnection()
    {
        Console.WriteLine("  - TCP SYN");
        await Task.Delay(10);
        Console.WriteLine("  - TCP SYN-ACK");
        await Task.Delay(10);
        Console.WriteLine("  - TCP ACK");
        await Task.Delay(10);
        Console.WriteLine("  - TCP connection established");
    }
    
    private async Task SimulateTlsHandshake()
    {
        await SimulateTcpConnection();
        Console.WriteLine("  - TLS ClientHello");
        await Task.Delay(20);
        Console.WriteLine("  - TLS ServerHello, Certificate, ServerHelloDone");
        await Task.Delay(20);
        Console.WriteLine("  - TLS ClientKeyExchange, ChangeCipherSpec, Finished");
        await Task.Delay(20);
        Console.WriteLine("  - TLS ChangeCipherSpec, Finished");
        await Task.Delay(20);
        Console.WriteLine("  - TLS handshake completed");
    }
    
    private Dictionary<string, string> ParseQueryString(string query)
    {
        var result = new Dictionary<string, string>();
        if (string.IsNullOrEmpty(query) || query == "?")
            return result;
            
        query = query.TrimStart('?');
        var pairs = query.Split('&');
        
        foreach (var pair in pairs)
        {
            var keyValue = pair.Split('=');
            if (keyValue.Length == 2)
            {
                var key = HttpUtility.UrlDecode(keyValue[0]);
                var value = HttpUtility.UrlDecode(keyValue[1]);
                result[key] = value;
            }
        }
        
        return result;
    }
}