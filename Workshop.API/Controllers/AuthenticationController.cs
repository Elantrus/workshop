using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Features;

namespace Workshop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUser.AuthenticateUserCommand authenticateUserCommand)
    {
        var result = await _mediator.Send(authenticateUserCommand);
        return Ok(result);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshUser.RefreshUserCommand refreshUserCommand)
    {
        var result = await _mediator.Send(refreshUserCommand);
        return Ok(result);
    }
    
}