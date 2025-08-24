using System;

namespace DesignPatterns.DecoratorSample;

public class BasicNotifier : INotifierComponent
{
    public void Send(string message)
    {
        Console.WriteLine($"Basic notification: {message}");
    }
}
