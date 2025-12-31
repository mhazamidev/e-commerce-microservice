using Domain.SeedWork.Events;
using ProductService.Domain.Products.ValueObjects;

namespace ProductService.Domain.Products.Events;

public record ProductPriceChangedDomainEvent(ProductId Id, decimal Price) : DomainEvent;
