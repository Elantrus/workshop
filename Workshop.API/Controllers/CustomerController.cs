using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Contracts;

namespace Workshop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerController(IMediator mediator)
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