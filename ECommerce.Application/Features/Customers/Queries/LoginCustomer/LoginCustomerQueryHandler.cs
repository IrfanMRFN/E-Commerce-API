using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.Customers.Queries.LoginCustomer;

public class LoginCustomerQueryHandler : IRequestHandler<LoginCustomerQuery, string>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCustomerQueryHandler(ICustomerRepository customerRepository, IPasswordHasher passwordHasher)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Handle(LoginCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByEmailAsync(request.Email);

        if (customer == null || !_passwordHasher.Verify(customer.PasswordHash, request.Password))
            throw new UnauthorizedAccessException("Invalid email or password.");

        // TODO: Generate actual JWT Token.
        return "fake-jwt-token-for-" + customer.Id;
    }
}
