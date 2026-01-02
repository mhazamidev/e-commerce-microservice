using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using OrderService.Domain.Orders.Entities;

namespace OrderService.Infrastructure.Persistence.Configurations;

public static class OrderClassMap
{
    public static void Register()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(Order)))
            return;

        BsonClassMap.RegisterClassMap<Order>(cm =>
        {
            cm.AutoMap();

            cm.MapIdProperty(x => x.Id)
              .SetSerializer(new GuidSerializer(GuidRepresentation.Standard))
              .SetElementName("_id");

            cm.MapProperty(x => x.CustomerId)
              .SetSerializer(new GuidSerializer(GuidRepresentation.Standard))
              .SetElementName("customerId");

            cm.MapProperty(x => x.Status)
              .SetSerializer(new Int32Serializer())
              .SetElementName("status");

            cm.MapProperty(x => x.TotalAmount)
              .SetElementName("totalAmount");

            cm.MapField("_details")
              .SetElementName("details");

            cm.SetIgnoreExtraElements(true);
        });
    }
}