namespace PublisherSubscriberSample;

public class Subscriber(string name)
{
    private readonly string _name = name;

    public void Subscribe(Publisher publisher)
    {
        publisher.OnPublish += (sender, e) =>
        {
            Console.WriteLine($"Subscriber '{_name}' received event with data: {e.Data}");
        };
    }
}