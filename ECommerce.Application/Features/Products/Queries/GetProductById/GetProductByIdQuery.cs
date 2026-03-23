using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>
{
}
