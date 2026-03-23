using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;

public record ProductDto
(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity
);