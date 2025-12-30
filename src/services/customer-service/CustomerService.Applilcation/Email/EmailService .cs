using CustomerService.Applilcation.Core.Exceptions;
using CustomerService.Infrastructure.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace CustomerService.Applilcation.Email;

public sealed class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }
    public async Task SendEmailChangedNotificationAsync(string email)
    {
        var subject = "Welcome!";
        var body = "Welcome to our platform. We're glad to have you.";

        await SendGenericEmailAsync(email, subject, body);
    }

    public async Task SendGenericEmailAsync(string to, string subject, string body)
    {
        try
        {
            // 🔹 اینجا عمداً fake / placeholder هست
            // بعداً می‌تونی SMTP / SendGrid / SES بزنی

            _logger.LogInformation(
                "Sending email to {To}. Subject: {Subject}",
                to,
                subject);

            await Task.Delay(50); // simulate I/O
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {To}", to);

            throw new ApplicationDataException(
                $"Failed to send email to {to}",
                innerException: ex);
        }
    }

    public async Task SendWelcomeEmailAsync(string email)
    {
        var subject = "Welcome!";
        var body = "Welcome to our platform. We're glad to have you.";

        await SendGenericEmailAsync(email, subject, body);
    }
}
