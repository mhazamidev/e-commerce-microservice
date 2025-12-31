using ProductService.Domain.Products.ValueObjects;
using ProductWebApi.Domain.Entities;

namespace ProductService.Domain.Repositories;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken = default);
    Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetActivesAsync(CancellationToken cancellationToken = default);
    void Update(Product product);
}
