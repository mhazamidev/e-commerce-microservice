using OrderService.Domain.Orders.Entities;
using OrderService.Domain.Orders.ValueObjects;

namespace OrderService.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetAsync(OrderId id, CancellationToken ct = default);
    Task AddAsync(Order order, CancellationToken ct = default);
    void Update(Order order);
}
