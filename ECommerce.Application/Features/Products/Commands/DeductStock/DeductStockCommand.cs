using MediatR;

namespace ECommerce.Application.Features.Products.Commands.DeductStock;

public record DeductStockCommand(Guid Id, int Quantity) : IRequest;
