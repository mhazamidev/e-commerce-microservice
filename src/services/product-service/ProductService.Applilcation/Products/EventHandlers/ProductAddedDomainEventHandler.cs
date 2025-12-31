using MediatR;
using Microsoft.Extensions.Logging;
using ProductService.Domain.Products.Events;

namespace ProductService.Applilcation.Products.EventHandlers;

internal class ProductAddedDomainEventHandler(ILogger<ProductAddedDomainEventHandler> _logger) : INotificationHandler<ProductAddedDomainEvent>
{
    public Task Handle(ProductAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
           "New Product added {Id}. Name: {Name}",
           notification.Id,
           notification.Name
       );

        return Task.CompletedTask;
    }
}
