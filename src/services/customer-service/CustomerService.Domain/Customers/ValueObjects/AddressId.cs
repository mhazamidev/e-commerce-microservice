using Domain.SeedWork;

namespace CustomerService.Domain.Customers.ValueObjects;

public sealed class AddressId : StronglyTypedId<AddressId>
{
    public AddressId(Guid value) : base(value)
    {
    }

    public static AddressId New() => new AddressId(Guid.NewGuid());
}
