using CustomerService.Domain.Customers.ValueObjects;
using Domain.SeedWork.Events;

namespace CustomerService.Domain.Customers.Events;

public sealed record CustomerEmailChangedDomainEvent(CustomerId CustomerId, Email NewEmail) : DomainEvent;
