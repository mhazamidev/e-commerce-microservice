using Domain.SeedWork;
using ProductService.Domain.Products.Events;
using ProductService.Domain.Products.ValueObjects;

namespace ProductWebApi.Domain.Entities;

public sealed class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }

    private Product() : base(new ProductId(Guid.Empty)) { }


    private Product(ProductId id, string name, decimal price, bool isActive) : base(id)
    {
        Name  = name;
        Price = price;
        IsActive = isActive;

        AddDomainEvent(new ProductAddedDomainEvent(Id, Name));
    }


    public static Product Create(string name, decimal price, bool isActive)
    {
        return new Product(
            ProductId.New(),
            name,
            price,
            isActive
        );
    }

    public void ChangeName(string name)
    {

        if (Name == name)
            return;

        Name = name;

        AddDomainEvent(new ProductNameChangedDomainEvent(Id, Name));
    }

    public void ChangePrice(decimal price)
    {

        if (Price == price)
            return;

        Price = price;

        AddDomainEvent(new ProductPriceChangedDomainEvent(Id, Price));
    }

    public void Active()
    {
        if (IsActive)
            return;

        IsActive = true;
    }

    public void Inactive()
    {
        if (!IsActive)
            return;

        IsActive = false;
    }
}
