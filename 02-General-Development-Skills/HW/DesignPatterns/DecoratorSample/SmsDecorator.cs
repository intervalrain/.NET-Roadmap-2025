using System;

namespace DesignPatterns.DecoratorSample;

public class SmsDecorator : NotifierDecorator
{
    public SmsDecorator(INotifierComponent component) : base(component) { }

    public override void Send(string message)
    {
        base.Send(message);
        Console.WriteLine($"SMS: {message}");
    }
}
