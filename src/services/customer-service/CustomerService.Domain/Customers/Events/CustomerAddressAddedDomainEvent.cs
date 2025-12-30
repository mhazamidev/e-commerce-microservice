using CustomerService.Domain.Customers.ValueObjects;
using Domain.SeedWork.Events;

namespace CustomerService.Domain.Customers.Events;

public sealed record CustomerAddressAddedDomainEvent(CustomerId CustomerId, AddressId AddressId) : DomainEvent;
