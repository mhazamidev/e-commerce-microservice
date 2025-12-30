using CustomerService.Domain.Customers.Events;
using CustomerService.Domain.Customers.ValueObjects;
using Domain.SeedWork;

namespace CustomerService.Domain.Customers.Entities;

public sealed class Customer : AggregateRoot<CustomerId>
{
    private readonly List<Address> _addresses = new();
    public FullName Name { get; private set; }
    public Email Email { get; private set; }
    public bool Enabled { get; private set; }
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
    private Customer() : base(new CustomerId(Guid.Empty)) { }

    private Customer(CustomerId id, FullName name, Email email, bool enabled) : base(id)
    {
        Name  = name;
        Email = email;
        Enabled = enabled;

        AddDomainEvent(new CustomerRegisteredDomainEvent(Id, Email));
    }

    public static Customer Create(string firstName, string lastName, string email, bool enabled)
    {
        return new Customer(
            CustomerId.New(),
            FullName.Create(firstName, lastName),
            Email.Create(email),
            enabled
        );
    }

    public void ChangeName(string firstName, string lastName)
    {
        var name = FullName.Create(firstName, lastName);

        if (Name == name)
            return;

        Name = name;
    }

    public void ChangeEmail(string newEmail)
    {
        var email = Email.Create(newEmail);

        if (Email == email)
            return;

        Email = email;
        AddDomainEvent(new CustomerEmailChangedDomainEvent(Id, Email));
    }

    public void Enable()
    {
        if (Enabled)
            return;

        Enabled = true;
    }

    public void Disable()
    {
        if (!Enabled)
            return;

        Enabled = false;
    }


    public static Customer CreateReference(CustomerId id)
    {
        return new Customer
        {
            Id = id
        };
    }



    public Address AddAddress(
           string line1,
           string? line2,
           string city,
           string? state,
           string postalCode,
           string country,
           bool isPrimary = false)
    {
        var address = Address.Create(
            Id,
            line1, line2, city, state, postalCode, country);

        if (isPrimary || !_addresses.Any())
            SetPrimaryAddress(address);

        _addresses.Add(address);

        AddDomainEvent(new CustomerAddressAddedDomainEvent(Id, address.Id));

        return address;
    }

    private void SetPrimaryAddress(Address address)
    {
        foreach (var addr in _addresses)
            addr.UnmarkPrimary();

        address.MarkPrimary();
    }
}
