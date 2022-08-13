using Microsoft.AspNetCore.Mvc;
using Users.Contracts;

namespace Workshop.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUser.Command command)
    {
        
    }
}