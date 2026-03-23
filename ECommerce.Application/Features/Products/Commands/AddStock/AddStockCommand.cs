using MediatR;

namespace ECommerce.Application.Features.Products.Commands.AddStock;

public record AddStockCommand(Guid Id, int Quantity) : IRequest;