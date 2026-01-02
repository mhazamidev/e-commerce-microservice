using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using OrderService.Domain.Orders.Entities;

namespace OrderService.Infrastructure.Persistence.Context;

public class MongoContext : IMongoContext
{
    private readonly IMongoDatabase _database;
    public MongoContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["Mongo:ConnectionString"]);
        _database = client.GetDatabase(configuration["Mongo:Database"]);
    }
    public IMongoCollection<Order> Orders
         => _database.GetCollection<Order>("orders");
}
