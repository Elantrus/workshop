using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Features;
using Workshop.API.Extensions;

namespace Workshop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "admin")]
public class AdministratorController : ControllerBase
{
    private readonly IMediator _mediator;
    public AdministratorController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAdmin([FromBody] CreateAdmin.Command createAdminCommand)
    {
        var result = await _mediator.Send(createAdminCommand);
        return Ok(result);
    }
}