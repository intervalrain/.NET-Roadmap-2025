namespace CalculatorSample;

public class Program
{
    static List<Product> Generate(int num)
    {
        if (num <= 0 || num > 100) throw new ArgumentOutOfRangeException(nameof(num));

        var rand = new Random();
        var products = Enumerable.Range(1, num).Select(i => new Product
        {
            Name = "Product " + i,
            Price = rand.Next(0, 10),
        });

        return products.ToList();
    }

    public static void Main(string[] args)
    {
        int num;

        if (args.Length == 0 || args.Length > 1 || !int.TryParse(args[0], out num))
        {
            num = 5;
        }

        var calculator = new Calculator();

        var products = Generate(num);

        var total = calculator.Calculate(products, out var message);

        Console.WriteLine($"The total of products are {total}. {message}");
    }
}