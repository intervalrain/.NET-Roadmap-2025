using DnsDemo.Interfaces;
using DnsDemo.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace DnsDemo;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Async(c => c.Console())
            .CreateBootstrapLogger();

        try
        {
            Log.Information("[Program] 🌐 DNS and Web Server Demo");
            Log.Information("[Program] {Separator}", new string('=', 100));

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices)
                .UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(context.Configuration);
                });

            Log.Information("[Program] Application host created successfully");

            var app = host.Build();
            await app.RunAsync();

            Log.Information("[Program] Application completed successfully");

            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IBrowser, Browser>();
        services.AddTransient<IDnsResolver, DnsResolver>();
        services.AddTransient<IRootServer, RootServer>();
        services.AddTransient<ITldServerFactory, TldServerFactory>();
        services.AddTransient<IAuthNameServer, AuthNameServer>();
        services.AddTransient<IWebServer, WebServer>();
        services.AddTransient<IAspNetCoreApp, AspNetCoreApp>();
        
        services.AddHostedService<BrowserDemoService>();
    }
}