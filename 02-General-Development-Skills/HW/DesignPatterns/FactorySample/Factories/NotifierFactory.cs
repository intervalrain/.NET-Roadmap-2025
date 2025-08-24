using FactorySample.Notifiers;

namespace FactorySample.Factories;

public abstract class NotifierFactory
{
    public abstract INotifier CreateNotifier();
}