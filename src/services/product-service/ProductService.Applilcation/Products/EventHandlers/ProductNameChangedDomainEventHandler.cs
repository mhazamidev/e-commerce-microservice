using MediatR;
using Microsoft.Extensions.Logging;
using ProductService.Domain.Products.Events;

namespace ProductService.Applilcation.Products.EventHandlers;

internal class ProductNameChangedDomainEventHandler(ILogger<ProductNameChangedDomainEventHandler> _logger) : INotificationHandler<ProductNameChangedDomainEvent>
{
    public Task Handle(ProductNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
           "Name changed for product {Id}. New name: {Name}",
           notification.Id,
           notification.Name
       );

        return Task.CompletedTask;
    }
}
