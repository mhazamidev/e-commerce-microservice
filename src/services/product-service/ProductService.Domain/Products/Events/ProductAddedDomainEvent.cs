using Domain.SeedWork.Events;
using ProductService.Domain.Products.ValueObjects;

namespace ProductService.Domain.Products.Events;

public record ProductAddedDomainEvent(ProductId Id, string Name) : DomainEvent;

