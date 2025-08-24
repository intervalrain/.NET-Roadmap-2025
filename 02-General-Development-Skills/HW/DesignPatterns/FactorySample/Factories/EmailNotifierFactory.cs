using FactorySample.Notifiers;

namespace FactorySample.Factories;

public class EmailNotifierFactory : NotifierFactory
{
    public override INotifier CreateNotifier()
    {
        return new EmailNotifier();
    }
}