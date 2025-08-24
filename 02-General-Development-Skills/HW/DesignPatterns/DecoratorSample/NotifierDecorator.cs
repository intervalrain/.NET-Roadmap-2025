namespace DesignPatterns.DecoratorSample;

public abstract class NotifierDecorator : INotifierComponent
{
    protected readonly INotifierComponent _component;

    protected NotifierDecorator(INotifierComponent component)
    {
        _component = component;
    }

    public virtual void Send(string message)
    {
        _component.Send(message);
    }
}
