using Domain.SeedWork;

namespace OrderService.Domain.Orders.ValueObjects;

public sealed class Money : ValueObject
{
    public decimal Value { get; }
    public string Currency { get; }

    private Money(decimal value, string currency)
    {
        if (value < 0)
            throw new DomainException("Money cannot be negative");

        Value = value;
        Currency = currency;
    }

    public static Money From(decimal value, string currency = "USD")
    {
        return new Money(decimal.Round(value, 2), currency);
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return From(Value + other.Value, Currency);
    }

    public Money Multiply(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero");

        return From(Value * quantity, Currency);
    }

    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new DomainException("Currency mismatch");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }

    public override string ToString()
        => $"{Value:N2} {Currency}";
}

