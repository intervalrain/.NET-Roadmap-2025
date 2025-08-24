namespace FactorySample.Notifiers;

public class SmsNotifier : INotifier
{
    public void Notify(string message)
    {
        Console.WriteLine($"SMS: {message}");
    }
}