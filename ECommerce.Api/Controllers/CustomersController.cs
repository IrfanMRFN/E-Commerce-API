using ECommerce.Application.Features.Customers.Commands.CreateCustomer;
using ECommerce.Application.Features.Customers.Queries.LoginCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ISender _mediator;

    public CustomersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateCustomerCommand command)
    {
        var customerId = await _mediator.Send(command);
        return Ok(new { id = customerId });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCustomerQuery query)
    {
        var token = await _mediator.Send(query);
        return Ok(new { token });
    }
}
