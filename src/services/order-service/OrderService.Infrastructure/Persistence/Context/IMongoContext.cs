using MongoDB.Driver;
using OrderService.Domain.Orders.Entities;

namespace OrderService.Infrastructure.Persistence.Context;

public interface IMongoContext
{
    IMongoCollection<Order> Orders { get; }
}
