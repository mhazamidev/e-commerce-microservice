using Domain.SeedWork.Events;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.Domain.Entities;
using System.Reflection;

namespace ProductService.Infrastructure.Persistence.Context;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }
    public virtual DbSet<StoredEvent> StoredEvents => Set<StoredEvent>();
    public virtual DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
