using Domain.SeedWork;

namespace OrderService.Domain.Orders.ValueObjects;

public sealed class ProductId : ValueObject
{
    public Guid Value { get; }

    private ProductId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("ProductId cannot be empty");

        Value = value;
    }

    public static ProductId From(Guid value)
        => new ProductId(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
