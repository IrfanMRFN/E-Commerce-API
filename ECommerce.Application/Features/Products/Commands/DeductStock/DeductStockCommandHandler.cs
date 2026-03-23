using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.DeductStock;

public class DeductStockCommandHandler : IRequestHandler<DeductStockCommand>
{
    private readonly IProductRepository _productRepository;

    public DeductStockCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(DeductStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
            throw new KeyNotFoundException($"Product with ID {request.Id} was not found.");

        product.DeductStock(request.Quantity);

        await _productRepository.UpdateAsync(product);
    }
}
