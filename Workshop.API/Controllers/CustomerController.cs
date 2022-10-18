using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Features;
using Workshop.API.Extensions;

namespace Workshop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "customer")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomer.CreateCustomerCommand createCustomerCreateCustomerCommand)
    {
        var result = await _mediator.Send(createCustomerCreateCustomerCommand);
        return Ok(result);
    }

}