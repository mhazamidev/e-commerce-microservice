using CustomerService.Domain.Repositories;

namespace CustomerService.Infrastructure.Persistence.UOW;

public interface ICustomerUnitOfWok : IUnitOfWork
{
    public ICustomerRepository Customers { get; }
    public IAddressRepository Addresses { get; }
}
