using Domain.SeedWork;

namespace CustomerService.Domain.Customers.ValueObjects;

public sealed class CustomerId : StronglyTypedId<CustomerId>
{
    public CustomerId(Guid value) : base(value)
    {
    }
    public static CustomerId New() => new CustomerId(Guid.NewGuid());
}
