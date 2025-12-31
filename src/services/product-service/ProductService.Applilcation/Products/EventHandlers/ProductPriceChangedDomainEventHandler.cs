using MediatR;
using Microsoft.Extensions.Logging;
using ProductService.Domain.Products.Events;

namespace ProductService.Applilcation.Products.EventHandlers;

internal class ProductPriceChangedDomainEventHandler(ILogger<ProductPriceChangedDomainEventHandler> _logger) : INotificationHandler<ProductPriceChangedDomainEvent>
{
    public Task Handle(ProductPriceChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
           "Price changed for product {Id}. New price: {Price}",
           notification.Id,
           notification.Price
       );

        return Task.CompletedTask;
    }
}
