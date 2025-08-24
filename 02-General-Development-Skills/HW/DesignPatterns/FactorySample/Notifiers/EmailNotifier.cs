namespace FactorySample.Notifiers;

public class EmailNotifier : INotifier
{
    public void Notify(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}