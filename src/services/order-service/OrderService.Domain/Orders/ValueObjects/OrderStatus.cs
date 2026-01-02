using Domain.SeedWork;

namespace OrderService.Domain.Orders.ValueObjects;

public sealed class OrderStatus : ValueObject
{
    public static readonly OrderStatus Draft = new("Draft", 0);
    public static readonly OrderStatus Paid = new("Paid", 1);
    public static readonly OrderStatus Shipped = new("Shipped", 2);
    public static readonly OrderStatus Completed = new("Completed", 3);
    public static readonly OrderStatus Canceled = new("Canceled", 4);

    public string Name { get; }
    public int Value { get; }


    private OrderStatus(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public bool CanAddItem =>
        this == Draft;

    public bool CanPay =>
        this == Draft;

    public bool CanShip =>
        this == Paid;

    public bool CanComplete =>
        this == Shipped;

    public bool CanCancel =>
        this == Draft || this == Paid;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static OrderStatus FromValue(int value)
    {
        return value switch
        {
            0 => Draft,
            1 => Paid,
            2 => Shipped,
            3 => Completed,
            4 => Canceled,
            _ => throw new DomainException("Invalid order status")
        };
    }
}
