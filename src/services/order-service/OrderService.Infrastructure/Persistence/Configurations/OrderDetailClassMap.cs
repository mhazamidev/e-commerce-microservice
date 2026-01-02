using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using OrderService.Domain.Orders.Entities;

namespace OrderService.Infrastructure.Persistence.Configurations;

public static class OrderDetailClassMap
{
    public static void Register()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(OrderDetail)))
            return;

        BsonClassMap.RegisterClassMap<OrderDetail>(cm =>
        {
            cm.AutoMap();

            cm.MapIdProperty(x => x.Id)
              .SetSerializer(new GuidSerializer(GuidRepresentation.Standard))
              .SetElementName("id");

            cm.MapProperty(x => x.ProductId)
              .SetSerializer(new GuidSerializer(GuidRepresentation.Standard))
              .SetElementName("productId");

            cm.MapProperty(x => x.Quantity)
              .SetElementName("quantity");

            cm.MapProperty(x => x.UnitPrice)
              .SetElementName("unitPrice");

            cm.SetIgnoreExtraElements(true);
        });
    }
}
