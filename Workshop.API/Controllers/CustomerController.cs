using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Contracts;
using Workshop.API.Extensions;

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
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomer.CreateCustomerCommand createCustomerCommand)
    {
        var result = await _mediator.Send(createCustomerCommand);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomer()
    {
        var userId = User.Claims.GetUserId();
        var getUserCommand = new GetCustomer.GetCustomerCommand { UserId =  userId };
        var result = await _mediator.Send(getUserCommand);
        return Ok(result);
    }
}