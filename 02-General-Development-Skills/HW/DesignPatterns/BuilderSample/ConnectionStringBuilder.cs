namespace BuilderSample;

public class ConnectionStringBuilder
{
    private readonly ConnectionInfo _info = new ConnectionInfo();

    public ConnectionStringBuilder WithHost(string host)
    {
        _info.Host = host;
        return this;
    }

    public ConnectionStringBuilder WithPort(int port)
    {
        _info.Port = port;
        return this;
    }

    public ConnectionStringBuilder WithDatabase(string database)
    {
        _info.Database = database;
        return this;
    }

    public ConnectionStringBuilder WithUser(string username, string password)
    {
        _info.Username = username;
        _info.Password = password;
        return this;
    }

    public ConnectionInfo Build() => _info;
}