namespace DnsDemo.Interfaces;

public interface ITldServerFactory
{
    ITldServer CreateTldServer(string serverAddress);
}