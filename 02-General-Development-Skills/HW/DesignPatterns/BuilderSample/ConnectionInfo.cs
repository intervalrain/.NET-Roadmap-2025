namespace BuilderSample;

public class ConnectionInfo
{
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 5432;
    public string Database { get; set; } = "Postgres";
    public string Username { get; set; } = "postgres";
    public string Password { get; set; } = "changeme";

    public string ToConnectionString() => $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
}