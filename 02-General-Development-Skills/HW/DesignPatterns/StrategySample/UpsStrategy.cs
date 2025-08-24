using StrategySample.Models;

namespace StrategySample;

public class UpsStrategy : IShippingStrategry
{
    private static readonly Lazy<UpsStrategy> _lazyInstance = new(() => new UpsStrategy());
    public static UpsStrategy Instance => _lazyInstance.Value;
    private UpsStrategy() { }

    public decimal Calculate(Order order) => 6.5m;
}