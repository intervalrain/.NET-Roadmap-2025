namespace PublisherSubscriberSample;

public class Publisher
{
    public event EventHandler<Event>? OnPublish;

    public void Publish(string data)
    {
        Console.WriteLine($"Publishing event with data: {data}");
        OnPublish?.Invoke(this, new Event(data));
    }
}