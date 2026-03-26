using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Customer?> GetByEmailAsync(string email);
    Task AddAsync(Customer customer);
}
