using Bogus;

using Microsoft.AspNetCore.Mvc;

using RouteDemo.Models;

namespace RouteDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private static readonly List<User> _users = GenerateUser();
    private static List<User> GenerateUser()
    {
        var faker = new Faker<User>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Name, f => f.Name.FirstName())
            .RuleFor(u => u.Email, f => $"{f.Name.FirstName()}@example.com")
            .RuleFor(u => u.Address, f => f.Address.FullAddress());
        var users = Enumerable.Range(1, 5).Select(i => faker.Generate()).ToList();

        return users;
    }

    [HttpGet]
    public IEnumerable<User> GetUsers()
    {
        return _users;
    }

    [HttpGet("{id:guid}")]
    public ActionResult<User?> GetUser(Guid id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    [HttpPost]
    public ActionResult CreateUser(CreateUpdateUserDto input)
    {
        _users.Add(new User
        {
            Name = input.Name,
            Email = input.Email,
            Address = input.Address
        });

        return Created();
    }

    [HttpPut("{id:guid}")]
    public ActionResult UpdateUser(Guid id, CreateUpdateUserDto user)
    {
        var u = _users.FirstOrDefault(u => u.Id == id) ?? throw new Exception("Entity not found");

        _users.Remove(u);
        _users.Add(new User
        {
            Name = user.Name,
            Email = user.Email,
            Address = user.Address
        });

        return NoContent();
    }

    [HttpDelete]
    public ActionResult DeleteUser(Guid id)
    {
        var u = _users.FirstOrDefault(u => u.Id == id) ?? throw new Exception("Entity not found");

        _users.Remove(u);

        return NoContent();
    }
}