using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Contracts;

namespace Workshop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUser.AuthenticateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
}