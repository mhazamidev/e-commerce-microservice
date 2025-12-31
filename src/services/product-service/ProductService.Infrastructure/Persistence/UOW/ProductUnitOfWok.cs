using Microsoft.Extensions.Logging;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Persistence.Context;
using ProductService.Infrastructure.Persistence.Repositories;

namespace ProductService.Infrastructure.Persistence.UOW;

public class ProductUnitOfWok : UnitOfWork<ProductDbContext>, IProductUnitOfWok
{
    private IProductRepository? _productRepository;
    private readonly ProductDbContext _dbContext;

    public ProductUnitOfWok(ProductDbContext dbContext, ILogger<ProductDbContext> logger) : base(dbContext, logger)
    {
        _dbContext = dbContext;
    }

    public IProductRepository Products
        => _productRepository ?? new ProductRepository(_dbContext);

}
