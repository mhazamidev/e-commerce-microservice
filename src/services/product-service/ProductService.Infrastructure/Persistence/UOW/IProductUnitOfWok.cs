using ProductService.Domain.Repositories;

namespace ProductService.Infrastructure.Persistence.UOW;

public interface IProductUnitOfWok : IUnitOfWork
{
    public IProductRepository Products { get; }
}
