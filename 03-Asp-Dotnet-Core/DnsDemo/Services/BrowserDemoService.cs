using DnsDemo.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DnsDemo.Services;

public class BrowserDemoService(IBrowser browser, ILogger<BrowserDemoService> logger) : BackgroundService
{
    private readonly string[] _testUrls = [
        "https://www.calculator.com/add?num1=4&num2=8",
        "https://example.com/page1",
        "https://www.calculator.com/multiply?num1=3&num2=5",
        "https://google.com/search?q=test",
        "https://example.com/page2",
        "https://www.calculator.com/add?num1=10&num2=20"
    ];

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("[Browser Demo Service] Starting interactive browsing mode...");
        logger.LogInformation(new string('=', 100));
        logger.LogInformation("Enter URLs to browse (or 'exit' to quit):");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.Write("\nEnter URL: ");
            var input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input) || input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
            
            logger.LogInformation("Accessing: {Url}", input);
            logger.LogInformation(new string('-', 50));
            
            try
            {
                await browser.RequestAsync(input);
            }
            catch (Exception ex)
            {
                logger.LogError("Error during request: {ErrorMessage}", ex.Message);
            }
        }
        
        logger.LogInformation("\n🎉 Interactive browsing session ended.");
    }
}