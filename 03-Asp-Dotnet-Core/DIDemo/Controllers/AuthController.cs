using DIDemo.Interfaces;
using DIDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DIDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, IUserService userService, IPasswordHasher passwordHasher) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly IUserService _userService = userService;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    [HttpGet("hash")]
    public IActionResult Hash([FromQuery] string password)
    {
        var (hash, salt) = _passwordHasher.HashPassword(password);

        return Ok(new
        {
            PasswordHash = hash,
            Salt = salt
        });
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        try
        {
            _authService.Register(request.Username, request.Email, request.Password);
            return Ok(new { message = "User registered successfully" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "An error occurred during registration" });
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            _authService.Login(request.Username, request.Password);
            return Ok(new { message = "Login successful" });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "An error occurred during login" });
        }
    }

    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
        try
        {
            var users = _userService.GetAllUsers();
            var userDtos = users.Select(u => new { u.Name, u.Email }).ToList();
            return Ok(userDtos);
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving users" });
        }
    }

    [HttpGet("users/{username}")]
    public IActionResult GetUser(string username)
    {
        try
        {
            var user = _userService.GetUser(username);
            if (user == null)
            {
                return NotFound(new { message = $"User {username} not found" });
            }

            return Ok(new { user.Name, user.Email });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving user" });
        }
    }
}