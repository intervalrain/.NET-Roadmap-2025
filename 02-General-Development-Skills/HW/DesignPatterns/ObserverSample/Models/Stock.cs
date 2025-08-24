namespace ObserverSample.Models;

public abstract class Stock
{
    private readonly List<IInvestor> _investors = new();

    public string Symbol { get; protected set; }
    public decimal Price { get; protected set; }

    protected Stock(string symbol, decimal price)
    {
        Symbol = symbol;
        Price = price;
    }

    public void Attach(IInvestor investor)
    {
        if (investor == null) return;
        _investors.Add(investor);
    }

    public void Detach(IInvestor investor)
    {
        if (investor == null) return;
        _investors.Remove(investor);
    }

    protected void Notify()
    {
        foreach (var investor in _investors)
        {
            investor.Update(this);
        }
    }

    public void SetPrice(decimal price)
    {
        Price = price;
        Notify();
    }
}