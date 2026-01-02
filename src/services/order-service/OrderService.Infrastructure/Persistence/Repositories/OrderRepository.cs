using MongoDB.Driver;
using OrderService.Domain.Orders.Entities;
using OrderService.Domain.Orders.ValueObjects;
using OrderService.Domain.Repositories;
using OrderService.Infrastructure.Persistence.Context;

namespace OrderService.Infrastructure.Persistence.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _collection;

    public OrderRepository(IMongoContext context)
    {
        _collection = context.Orders;
    }

    public async Task<Order?> GetAsync(OrderId id, CancellationToken ct = default)
    {
        return await _collection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync(ct);
    }

    public async Task AddAsync(Order order, CancellationToken ct = default)
    {
        await _collection.InsertOneAsync(order, cancellationToken: ct);
    }

    public void Update(Order order)
    {
        _collection.ReplaceOne(
            x => x.Id == order.Id,
            order);
    }
}