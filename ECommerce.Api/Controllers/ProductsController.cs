using ECommerce.Application.Features.Products.Commands.AddStock;
using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Features.Products.Commands.DeductStock;
using ECommerce.Application.Features.Products.Commands.DeleteProduct;
using ECommerce.Application.Features.Products.Commands.UpdateProduct;
using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using ECommerce.Application.Features.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ISender _mediator;

    public ProductsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var productId = await _mediator.Send(command);
        return Ok(new { id = productId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id)
            return BadRequest(new
            {
                title = "Bad Request",
                status = 400,
                detail = "The ID in the route does not match the ID in the request body."
            });

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{id:guid}/add-stock")]
    public async Task<IActionResult> AddStock(Guid id, [FromBody] AddStockCommand command)
    {
        if (id != command.Id)
            return BadRequest(new
            {
                title = "Bad Request",
                status = 400,
                detail = "The ID in the route does not match the ID in the request body."
            });

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{id:guid}/deduct-stock")]
    public async Task<IActionResult> DeductStock(Guid id, [FromBody] DeductStockCommand command)
    {
        if (id != command.Id)
            return BadRequest(new
            {
                title = "Bad Request",
                status = 400,
                detail = "The ID in the route does not match the ID in the request body."
            });

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}
