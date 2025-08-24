using StrategySample.Models;

namespace StrategySample;

public class Program
{
    public static void Main(string[] args)
    {
        var order = new Order();

        var calculator = new ShippingCostCalculator();

        // 1. use FedEx
        calculator.SetStrategy(FedExStrategry.Instance);

        Console.WriteLine(calculator.CalculateShippingCost(order));

        // 2. use Ups
        calculator.SetStrategy(UpsStrategy.Instance);

        Console.WriteLine(calculator.CalculateShippingCost(order));
    }
}