using CustomerService.Domain.Customers.Events;
using CustomerService.Infrastructure.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerService.Applilcation.Customers.EventHandlers;

public sealed class CustomerRegisteredDomainEventHandler : INotificationHandler<CustomerRegisteredDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<CustomerRegisteredDomainEventHandler> _logger;

    public CustomerRegisteredDomainEventHandler(
        IEmailService emailService,
        ILogger<CustomerRegisteredDomainEventHandler> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Handle(CustomerRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("New customer registered: {CustomerId} - {Email}",
            notification.CustomerId, notification.Email);

        await _emailService.SendWelcomeEmailAsync(notification.Email.Value);
    }
}
