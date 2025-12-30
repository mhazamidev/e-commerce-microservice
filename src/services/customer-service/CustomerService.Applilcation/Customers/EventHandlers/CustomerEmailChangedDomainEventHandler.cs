using CustomerService.Domain.Customers.Events;
using CustomerService.Infrastructure.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerService.Applilcation.Customers.EventHandlers;

public sealed class CustomerEmailChangedDomainEventHandler : INotificationHandler<CustomerEmailChangedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<CustomerEmailChangedDomainEventHandler> _logger;

    public CustomerEmailChangedDomainEventHandler(
        IEmailService emailService,
        ILogger<CustomerEmailChangedDomainEventHandler> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Handle(CustomerEmailChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Email changed for customer {CustomerId}. New email: {Email}",
            notification.CustomerId, notification.NewEmail);

        await _emailService.SendEmailChangedNotificationAsync(notification.NewEmail.Value);
    }
}
