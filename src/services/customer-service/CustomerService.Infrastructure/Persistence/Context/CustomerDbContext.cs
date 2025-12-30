using CustomerService.Domain.Customers.Entities;
using Domain.SeedWork.Events;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CustomerService.Infrastructure.Persistence.Context;

public class CustomerDbContext : DbContext
{
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {
    }
    public virtual DbSet<StoredEvent> StoredEvents => Set<StoredEvent>();
    public virtual DbSet<Customer> Customers => Set<Customer>();
    public virtual DbSet<Address> Addresses => Set<Address>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
