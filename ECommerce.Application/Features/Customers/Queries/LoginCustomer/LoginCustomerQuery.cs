using MediatR;

namespace ECommerce.Application.Features.Customers.Queries.LoginCustomer;

public record LoginCustomerQuery(
    string Email,
    string Password
) : IRequest<string>;
