using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Features;
using Workshop.API.Extensions;

namespace Workshop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        var userId = User.Claims.GetUserId();
        var getUserCommand = new GetUser.GetUserCommand { UserId =  userId };
        var result = await _mediator.Send(getUserCommand);
        return Ok(result);
    }
}