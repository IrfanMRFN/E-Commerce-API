using MediatR;

namespace ECommerce.Application.Features.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<Guid>;