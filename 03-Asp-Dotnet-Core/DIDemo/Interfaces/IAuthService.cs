namespace DIDemo.Interfaces;

public interface IAuthService
{
    public void Register(string username, string email, string password);
    public void Login(string username, string password);
}