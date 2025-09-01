namespace DIDemo.Models;

public class User
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;

    private User(string name, string email, string passwordHash, string salt)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
    }

    public static User Create(string name, string email, string passwordHash, string salt)
    {
        return new User(name, email, passwordHash, salt);
    }
}