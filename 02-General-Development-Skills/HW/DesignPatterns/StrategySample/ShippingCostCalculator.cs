using StrategySample.Models;

namespace StrategySample;

public class ShippingCostCalculator
{
    private IShippingStrategry _strategry;

    public void SetStrategy(IShippingStrategry strategy) => _strategry = strategy;

    public decimal CalculateShippingCost(Order order)
    {
        return _strategry.Calculate(order);
    }
}