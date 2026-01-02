using Domain.SeedWork;

namespace OrderService.Domain.Orders.ValueObjects;

public sealed class CustomerId : ValueObject
{
    public Guid Value { get; }

    private CustomerId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("CustomerId cannot be empty");

        Value = value;
    }

    public static CustomerId From(Guid value)
        => new CustomerId(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

