using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Contracts;

namespace Workshop.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateCustomer.CreateCustomerCommand createCustomerCommand)
    {
        var result = await _mediator.Send(createCustomerCommand);
        return Ok(result);
    }
}