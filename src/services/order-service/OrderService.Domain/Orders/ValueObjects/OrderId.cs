using Domain.SeedWork;

namespace OrderService.Domain.Orders.ValueObjects;

public sealed class OrderId : StronglyTypedId<OrderId>
{
    public OrderId(Guid value) : base(value)
    {
    }

    public static OrderId New() => new OrderId(Guid.NewGuid());
}
