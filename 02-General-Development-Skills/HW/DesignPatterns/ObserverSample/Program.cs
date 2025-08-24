using ObserverSample.Models;

namespace ObserverSample;

public class Program
{
    public static void Main(string[] args)
    {
        var names = args.Length == 0 ? ["guest"] : args;

        var builder = Host.CreateApplicationBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        builder.Services.AddSingleton<ICollection<IInvestor>>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<IInvestor>>();
            var investors = names.Select(investor => new Investor(investor, logger) as IInvestor).ToList();
            return investors;
        });
        builder.Services.AddHostedService<StockBoard>();

        var app = builder.Build();
        app.Run();

    }
}