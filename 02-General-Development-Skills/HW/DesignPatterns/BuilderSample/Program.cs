namespace BuilderSample;

public class Program
{
    public static void Main(string[] args)
    {
        var connectinoString = new ConnectionStringBuilder()
        .WithHost("Server")
        .WithPort(5432)
        .WithDatabase("Postgres")
        .WithUser("postgres", "changeme")
        .Build()
        .ToConnectionString();

        Console.WriteLine(connectinoString);
    }
}

