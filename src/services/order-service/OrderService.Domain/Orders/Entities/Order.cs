using Domain.SeedWork;
using OrderService.Domain.Orders.ValueObjects;

namespace OrderService.Domain.Orders.Entities;

public sealed class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderDetail> _details = new();

    public IReadOnlyCollection<OrderDetail> Details => _details.AsReadOnly();
    public OrderStatus Status { get; private set; }
    public Money TotalAmount { get; private set; }
    public CustomerId CustomerId { get; private set; }

    private Order() : base(new OrderId(Guid.Empty)) { }


    public static Order Create(CustomerId customerId)
    {
        return new Order
        {
            Id = OrderId.New(),
            Status = OrderStatus.Draft,
            CustomerId = customerId,
            TotalAmount = Money.From(0)
        };
    }

    public void AddItem(ProductId productId, int quantity, Money price)
    {
        if (Status != OrderStatus.Draft)
            throw new DomainException("Cannot modify finalized order");

        var detail = _details.FirstOrDefault(x => x.ProductId == productId);

        if (detail is null)
        {
            _details.Add(OrderDetail.Create(productId, quantity, price));
        }
        else
        {
            detail.IncreaseQuantity(quantity);
        }

        RecalculateTotal();
    }

    public void RemoveItem(ProductId productId)
    {
        var detail = _details.FirstOrDefault(x => x.ProductId == productId);
        if (detail is null) return;

        _details.Remove(detail);
        RecalculateTotal();
    }

    private void RecalculateTotal()
    {
        TotalAmount = Money.From(_details.Sum(x => x.Total.Value));
    }

    public void Pay()
    {
        if (!Status.CanPay)
            throw new DomainException("Order cannot be paid");

        Status = OrderStatus.Paid;
    }
}
