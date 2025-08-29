using System.Runtime.CompilerServices;

using ConfigDemo.Models;
using ConfigDemo.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ICurrentUserProvider _currentUserProvider;


    public TestController(ICurrentUserProvider currentUserProvider)
    {
        _currentUserProvider = currentUserProvider;
    }
    
    [HttpGet]
    public IActionResult GetCurrentUser()
    {
        var currentUser = _currentUserProvider.CurrentUser;
        return Ok(currentUser.Name);
    }
}