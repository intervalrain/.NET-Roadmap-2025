namespace ObserverSample.Models;

public class ConcreteStock : Stock
{
    public ConcreteStock(string symbol, decimal price) : base(symbol, price)
    {
    }

    public void Simulate()
    {
        var rand = new Random();
        var change = (decimal)(rand.NextDouble() * 10.0 - 5.0);
        var newPrice = Math.Round(Price + change, 2);
        SetPrice(newPrice);

    }
}
