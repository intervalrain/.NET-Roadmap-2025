using FactorySample.Factories;

namespace FactorySample;

public class Program
{
    // 1. 簡單使用 (直接指定工廠)
    static void SimpleUsage()
    {
        NotifierFactory factory = new EmailNotifierFactory();
        var notifier = factory.CreateNotifier();
        notifier.Notify("Welcome!");
    }

    // 2. 根據執行時參數選擇工廠
    static void UsageFromArgs(string[] args)
    {
        NotifierFactory factory = args.Length > 0 && args[0].Equals("sms", StringComparison.OrdinalIgnoreCase)
            ? new SmsNotifierFactory()
            : new EmailNotifierFactory();
        var notifier = factory.CreateNotifier();
        notifier.Notify($"Selected via args: {(args.Length > 0 ? args[0] : "email")}");
    }

    // 3. 一次建立多個工廠並迭代使用(多型)
    static void IterateFactories()
    {
        var factories = new NotifierFactory[]
        {
            new EmailNotifierFactory(),
            new SmsNotifierFactory()
        };
        foreach (var factory in factories)
        {
            var notifier = factory.CreateNotifier();
            notifier.Notify("Broadcast message");
        }
    }

    public static void Main(string[] args)
    {
        SimpleUsage();
        UsageFromArgs(args);
        IterateFactories();
    }
}