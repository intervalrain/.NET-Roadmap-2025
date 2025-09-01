using DIDemo.Models;

namespace DIDemo.Interfaces;

public interface IUserRepository
{
    User AddUser(User user);
    bool DeleteUser(string name);
    List<User> GetAll();
    User? GetUser(string name);
    User? GetUserByEmail(string email);
    void UpdateUser(User user);
}