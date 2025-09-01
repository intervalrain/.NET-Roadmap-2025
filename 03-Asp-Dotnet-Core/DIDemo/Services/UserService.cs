using DIDemo.Interfaces;
using DIDemo.Models;

namespace DIDemo.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public List<User> GetAllUsers()
    {
        return _userRepository.GetAll();
    }

    public User? GetUser(string username)
    {
        return _userRepository.GetUser(username);
    }
}