using CustomerService.Domain.Customers.Entities;
using CustomerService.Domain.Customers.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerService.Infrastructure.Persistence.Configurations;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(nameof(Address));
        builder.HasKey(x => x.Id);

        builder.Property(c => c.Id)
         .ValueGeneratedNever()
         .HasConversion(
             id => id.Value,
             value => new AddressId(value));

        builder.Property(x => x.Line1)
            .IsRequired()
            .HasMaxLength(400);

        builder.Property(x => x.Line2)
            .HasMaxLength(400);

        builder.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Country)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.State)
            .HasMaxLength(50);


        builder.Property(x => x.PostalCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.IsPrimary)
            .IsRequired();


        builder.Property(x => x.CustomerId)
            .IsRequired()
            .HasColumnName("CustomerId")
            .HasConversion(
            id => id.Value,
            value => new CustomerId(value));

        builder.HasOne<Customer>()              
            .WithMany(c => c.Addresses)        
            .HasForeignKey(a => a.CustomerId)  
            .OnDelete(DeleteBehavior.Cascade);

    }
}
