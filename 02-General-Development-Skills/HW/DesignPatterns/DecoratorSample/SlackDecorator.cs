using System;

namespace DesignPatterns.DecoratorSample;

public class SlackDecorator : NotifierDecorator
{
    public SlackDecorator(INotifierComponent component) : base(component) { }

    public override void Send(string message)
    {
        base.Send(message);
        Console.WriteLine($"Slack: {message}");
    }
}
