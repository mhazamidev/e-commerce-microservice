using CustomerService.Domain.Repositories;
using CustomerService.Infrastructure.Persistence.Context;
using CustomerService.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace CustomerService.Infrastructure.Persistence.UOW;

public class CustomerUnitOfWok : UnitOfWork<CustomerDbContext>, ICustomerUnitOfWok
{
    private ICustomerRepository? _customerRepository;
    private readonly CustomerDbContext _dbContext;

    public CustomerUnitOfWok(CustomerDbContext dbContext, ILogger<CustomerDbContext> logger) : base(dbContext, logger)
    {
        _dbContext = dbContext;
    }

    public ICustomerRepository Customers
        => _customerRepository ?? new CustomerRepository(_dbContext);

}
