using System;

namespace DesignPatterns.DecoratorSample;

class Program
{
    public static void Main()
    {
        Console.WriteLine("DecoratorSample demo:");

        INotifierComponent basic = new BasicNotifier();
        basic.Send("Hello world");

        Console.WriteLine("\nAdd SMS decorator:");
        INotifierComponent sms = new SmsDecorator(basic);
        sms.Send("Hello via SMS");

        Console.WriteLine("\nAdd SMS + Slack decorators:");
        INotifierComponent smsSlack = new SlackDecorator(new SmsDecorator(basic));
        smsSlack.Send("Hello via SMS and Slack");
    }
}
