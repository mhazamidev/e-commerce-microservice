using Domain.SeedWork.Events;
using ProductService.Domain.Products.ValueObjects;

namespace ProductService.Domain.Products.Events;

public record ProductNameChangedDomainEvent(ProductId Id, string Name) : DomainEvent;
