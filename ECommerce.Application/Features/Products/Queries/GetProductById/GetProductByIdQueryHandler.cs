using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
            throw new KeyNotFoundException($"Product with ID {request.Id} was not found.");

        return new ProductDto
        (
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity
        );
    }
}
