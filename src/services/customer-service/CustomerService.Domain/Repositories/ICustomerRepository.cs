using CustomerService.Domain.Customers.Entities;
using CustomerService.Domain.Customers.ValueObjects;

namespace CustomerService.Domain.Repositories;

public interface ICustomerRepository
{
    Task<IReadOnlyList<Customer>> GetActiveCustomersAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Customer?> GetAsync(CustomerId id, CancellationToken cancellationToken = default);
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    void Update(Customer customer);
    Task DeleteAsync(CustomerId id, CancellationToken cancellationToken = default);
}
