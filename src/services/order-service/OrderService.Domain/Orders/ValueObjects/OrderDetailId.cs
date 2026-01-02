using Domain.SeedWork;

namespace OrderService.Domain.Orders.ValueObjects;

public sealed class OrderDetailId : StronglyTypedId<OrderDetailId>
{
    public OrderDetailId(Guid value) : base(value)
    {
    }

    public static OrderDetailId New() => new OrderDetailId(Guid.NewGuid());
}
