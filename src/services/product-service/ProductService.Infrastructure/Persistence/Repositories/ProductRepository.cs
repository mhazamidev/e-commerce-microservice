using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Products.ValueObjects;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Persistence.Context;
using ProductWebApi.Domain.Entities;

namespace ProductService.Infrastructure.Persistence.Repositories;

public class ProductRepository(ProductDbContext dbContext) : IProductRepository
{
    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        => await dbContext.Products.AddAsync(product, cancellationToken);


    public async Task<IEnumerable<Product>> GetActivesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Products
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Name == name, cancellationToken);
    }

    public void Update(Product product)
        => dbContext.Products.Update(product);

}
