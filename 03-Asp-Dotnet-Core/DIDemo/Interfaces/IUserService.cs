using DIDemo.Models;

namespace DIDemo.Interfaces;

public interface IUserService
{
    List<User> GetAllUsers();
    User? GetUser(string username);
}