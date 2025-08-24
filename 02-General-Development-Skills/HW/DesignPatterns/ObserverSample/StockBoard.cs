using System.Collections;

using ObserverSample.Models;

namespace ObserverSample;

public class StockBoard : BackgroundService
{
    private readonly ILogger<StockBoard> _logger;
    private readonly IReadOnlyList<IInvestor> _investors;
    public StockBoard(ILogger<StockBoard> logger, ICollection<IInvestor> investors)
    {
        _logger = logger;
        _investors = investors.ToList();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var msft = new ConcreteStock("MSFT", 100m);
        var tsmc = new ConcreteStock("TSM", 50m);
        var nvidia = new ConcreteStock("NV", 400m);

        var stocks = new ConcreteStock[] { msft, tsmc, nvidia};

        foreach (var investor in _investors)
        {
            foreach (var stock in stocks)
            {
                stock.Attach(investor);
            }
        }
        var rand = new Random();

        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var stock in stocks)
            {
                if (stock is ConcreteStock cStock) cStock.Simulate();
                _logger.LogInformation("{time}: {symbol} price updated to {price}", DateTimeOffset.Now, stock.Symbol, stock.Price);
            }

            await Task.Delay(1500, stoppingToken);
        }
    }
}
