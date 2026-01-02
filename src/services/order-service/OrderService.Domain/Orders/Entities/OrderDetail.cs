using Domain.SeedWork;
using OrderService.Domain.Orders.ValueObjects;

namespace OrderService.Domain.Orders.Entities;

public sealed class OrderDetail : Entity<OrderDetailId>
{
    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money Total => UnitPrice.Multiply(Quantity);


    private OrderDetail() : base(new OrderDetailId(Guid.Empty)) { }


    internal static OrderDetail Create(ProductId productId, int quantity, Money price)
    {
        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero");

        return new OrderDetail
        {
            Id = OrderDetailId.New(),
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = price
        };
    }

    internal void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Invalid quantity");

        Quantity += quantity;
    }
}
