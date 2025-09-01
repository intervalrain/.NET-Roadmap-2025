using DIDemo.Interfaces;
using DIDemo.Models;

namespace DIDemo.Services;

public class AuthService(ILogger<AuthService> logger, IUserRepository userRepository, IPasswordHasher passwordHasher) : IAuthService
{
    private readonly ILogger<AuthService> _logger = logger;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public void Login(string username, string password)
    {
        var user = _userRepository.GetUser(username);
        if (user == null)
        {
            _logger.LogWarning("Login attempt for non-existent user: {Username}", username);
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        if (!_passwordHasher.VerifyPassword(password, user.PasswordHash, user.Salt))
        {
            _logger.LogWarning("Failed login attempt for user: {Username}", username);
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        _logger.LogInformation("User {Username} logged in successfully", username);
    }

    public void Register(string username, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Either username, email and password cannot be empty");
        }

        var existingUser = _userRepository.GetUser(username);
        if (existingUser != null)
        {
            throw new InvalidOperationException($"User {username} already exists");
        }

        var existingUserWithEmail = _userRepository.GetUserByEmail(email);
        if (existingUserWithEmail != null)
        {
            throw new InvalidOperationException($"User with email {email} already exists");
        }

        var (hash, salt) = _passwordHasher.HashPassword(password);
        var user = User.Create(username, email, hash, salt);
        
        _userRepository.AddUser(user);
        _logger.LogInformation("User {Username} registered successfully", username);
    }
}