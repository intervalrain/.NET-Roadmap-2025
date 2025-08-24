using StrategySample.Models;

namespace StrategySample;

public class FedExStrategry : IShippingStrategry
{
    private static readonly Lazy<FedExStrategry> _lazyInstance = new(() => new FedExStrategry());
    public static FedExStrategry Instance => _lazyInstance.Value;
    private FedExStrategry() { }

    public decimal Calculate(Order order) => 5.0m;
}