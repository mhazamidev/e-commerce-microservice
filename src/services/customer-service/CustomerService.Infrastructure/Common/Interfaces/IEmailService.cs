namespace CustomerService.Infrastructure.Common.Interfaces;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(string email);
    Task SendEmailChangedNotificationAsync(string email);
    Task SendGenericEmailAsync(string to, string subject, string body);
}