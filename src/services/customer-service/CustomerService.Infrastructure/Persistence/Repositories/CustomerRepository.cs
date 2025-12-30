using CustomerService.Domain.Customers.Entities;
using CustomerService.Domain.Customers.ValueObjects;
using CustomerService.Domain.Repositories;
using CustomerService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Persistence.Repositories;

public class CustomerRepository(CustomerDbContext _dbContext) : ICustomerRepository
{
    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Customers.AddAsync(customer, cancellationToken);
    }

    public async Task Delete(CustomerId id, CancellationToken cancellationToken = default)
    {
        await _dbContext.Customers
              .Where(c => c.Id == id)
              .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Customer>> GetActiveCustomersAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers
              .Where(c => c.Enabled)
              .Skip((page - 1) * pageSize)
              .Take(pageSize)
              .ToListAsync(cancellationToken);
    }

    public async Task<Customer?> GetAsync(CustomerId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Update(Customer customer)
    {
        _dbContext.Customers.Update(customer);
    }
}

