using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IPasswordHasher passwordHasher)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await _customerRepository.GetByEmailAsync(request.Email);
        if (existingCustomer != null)
            throw new InvalidOperationException("Email is already in use.");

        var hashedPassword = _passwordHasher.Hash(request.Password);
        var customer = new Customer(request.FirstName, request.LastName, request.Email, hashedPassword);

        await _customerRepository.AddAsync(customer);
        return customer.Id;
    }
}
