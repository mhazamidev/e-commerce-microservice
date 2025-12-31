using Domain.SeedWork;

namespace ProductService.Domain.Products.ValueObjects;

public sealed class ProductId : StronglyTypedId<ProductId>
{
    public ProductId(Guid value) : base(value)
    {
    }

    public static ProductId New() => new ProductId(Guid.NewGuid());
}
