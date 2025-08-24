using StrategySample.Models;

namespace StrategySample;

public interface IShippingStrategry
{
    decimal Calculate(Order order);
}