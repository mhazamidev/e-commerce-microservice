using CustomerService.Domain.Repositories;
using CustomerService.Infrastructure.Persistence.Context;

namespace CustomerService.Infrastructure.Persistence.Repositories;

public class AddressRepository(CustomerDbContext _dbContext) : IAddressRepository
{
}
