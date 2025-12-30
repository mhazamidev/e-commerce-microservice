using CustomerService.Domain.Customers.ValueObjects;
using Domain.SeedWork;

namespace CustomerService.Domain.Customers.Entities;

public sealed class Address : Entity<AddressId>
{
    public string Line1 { get; private set; }
    public string? Line2 { get; private set; }
    public string City { get; private set; }
    public string? State { get; private set; }
    public string PostalCode { get; private set; }
    public string Country { get; private set; }
    public bool IsPrimary { get; private set; }
    public CustomerId CustomerId { get; private set; }

    private Address() : base(new AddressId(Guid.Empty)) { } // EF Only

    private Address(
        AddressId id,
        CustomerId customerId,
        string line1,
        string? line2,
        string city,
        string? state,
        string postalCode,
        string country,
        bool isPrimary)
        : base(id)
    {
        CustomerId = customerId;
        Line1 = line1;
        Line2 = line2;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
        IsPrimary = isPrimary;
    }

    public static Address Create(
        CustomerId customerId,
        string line1,
        string? line2,
        string city,
        string? state,
        string postalCode,
        string country,
        bool isPrimary = false)
    {
        if (string.IsNullOrWhiteSpace(line1))
            throw new DomainException("Address line 1 is required.");

        return new Address(
            AddressId.New(),
            customerId,
            line1.Trim(),
            line2?.Trim(),
            city.Trim(),
            state?.Trim(),
            postalCode.Trim(),
            country.Trim(),
            isPrimary
        );
    }

    internal void MarkPrimary() => IsPrimary = true;
    internal void UnmarkPrimary() => IsPrimary = false;
}

