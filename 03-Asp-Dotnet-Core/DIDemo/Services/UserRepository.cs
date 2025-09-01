using DIDemo.Interfaces;
using DIDemo.Models;

namespace DIDemo.Services;

public class UserRepository : IUserRepository
{
    private static readonly Dictionary<string, User> _users = [];

    public List<User> GetAll()
    {
        return _users.Values.ToList();
    }

    public User? GetUser(string name)
    {
        return _users.TryGetValue(name, out var user) ? user : null;
    }

    public User AddUser(User user)
    {
        if (_users.ContainsKey(user.Name))
        {
            throw new Exception($"{user.Name} has already existed");
        }
        _users[user.Name] = user;

        return user;
    }

    public bool DeleteUser(string name)
    {
        return _users.Remove(name);
    }

    public void UpdateUser(User user)
    {
        if (!_users.ContainsKey(user.Name))
        {
            throw new Exception($"{user.Name} not exist");
        }
        _users[user.Name] = user;
    }

    public User? GetUserByEmail(string email)
    {
        var users = GetAll();
        var user = users.FirstOrDefault(u => u.Email == email);
        return user;
    }
}