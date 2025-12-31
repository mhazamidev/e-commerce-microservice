using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain.Products.ValueObjects;
using ProductWebApi.Domain.Entities;

namespace ProductService.Infrastructure.Persistence.Configurations;

internal class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));

        builder.HasKey(c => c.Id);

        builder.Property(c => c.IsActive)
           .IsRequired();

        builder.Property(c => c.Id)
           .ValueGeneratedNever()
           .HasConversion(
               id => id.Value,
               value => new ProductId(value));


        builder.Property(n => n.Name)
            .HasColumnName("Name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");


        builder.HasIndex(c => c.Name).IsUnique();
    }
}
