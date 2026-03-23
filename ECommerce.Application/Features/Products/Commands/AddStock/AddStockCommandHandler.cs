using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.AddStock;

public class AddStockCommandHandler : IRequestHandler<AddStockCommand>
{
    private readonly IProductRepository _productRepository;

    public AddStockCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(AddStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
            throw new KeyNotFoundException($"Product with ID {request.Id} was not found.");

        product.AddStock(request.Quantity);

        await _productRepository.UpdateAsync(product);
    }
}
