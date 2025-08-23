namespace CalculatorSample;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required double Price { get; set; }
}

public class Calculator
{
    public const double Discount95 = 0.95;
    public const double Discount90 = 0.9;
    public const int TotalOver100 = 100;
    public const int TotalOver50 = 50;

    public double Calculate(List<Product> products, out string message)
    {
        double total = products.Sum(p => p.Price);
        message = "";

        if (total > TotalOver100)
        {
            total *= Discount90;
            message = "You got 10% discount!";
        }
        else if (total > TotalOver50)
        {
            total *= Discount95;
            message = "You got 5% discount!";
        }
        return total;
    }
}