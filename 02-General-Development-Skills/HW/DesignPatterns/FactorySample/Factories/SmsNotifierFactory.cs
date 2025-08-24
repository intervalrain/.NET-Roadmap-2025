using FactorySample.Notifiers;

namespace FactorySample.Factories;

public class SmsNotifierFactory : NotifierFactory
{
    public override INotifier CreateNotifier()
    {
        return new SmsNotifier();
    }
}