using CustomerService.Domain.Customers.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerService.Applilcation.Customers.EventHandlers;

public sealed class CustomerAddressAddedDomainEventHandler : INotificationHandler<CustomerAddressAddedDomainEvent>
{
    private readonly ILogger<CustomerAddressAddedDomainEventHandler> _logger;

    public CustomerAddressAddedDomainEventHandler(
        ILogger<CustomerAddressAddedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerAddressAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "New address added for customer {CustomerId}. AddressId: {AddressId}",
            notification.CustomerId,
            notification.AddressId
        );

        return Task.CompletedTask;
    }
}
