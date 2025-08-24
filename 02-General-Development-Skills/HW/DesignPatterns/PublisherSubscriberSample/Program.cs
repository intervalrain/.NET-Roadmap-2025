namespace PublisherSubscriberSample;

class Program
{
    static void Main(string[] args)
    {
        var publisher = new Publisher();
        var subscriber1 = new Subscriber("Subscriber 1");
        var subscriber2 = new Subscriber("Subscriber 2");

        subscriber1.Subscribe(publisher);
        subscriber2.Subscribe(publisher);

        publisher.Publish("First Event");
        publisher.Publish("Second Event");
    }
}
