namespace SingletonSample;

public class Program
{
    public static void Main(string[] args)
    {
        var settings = AppSettings.Instance;

        var settings2 = AppSettings.Instance;

        Console.WriteLine(settings.ConnectionString);

        Console.WriteLine(settings.Equals(settings2) ? "the same" : "different");
    }
}