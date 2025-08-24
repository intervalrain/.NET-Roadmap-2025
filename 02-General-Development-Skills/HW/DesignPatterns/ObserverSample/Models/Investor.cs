namespace ObserverSample.Models;

public class Investor : IInvestor
{
    private readonly string _name;
    private readonly ILogger? _logger;

    public Investor(string name, ILogger? logger = null)
    {
        _name = name;
        _logger = logger;
    }

    public void Update(Stock stock)
    {
        if (_logger != null)
        {
            _logger.LogInformation("{name} notified: {symbol} -> {price}", _name, stock.Symbol, stock.Price);
        }
        else
        {
            Console.WriteLine($"{_name} notified: {stock.Symbol} -> {stock.Price}");
        }
    }
}
