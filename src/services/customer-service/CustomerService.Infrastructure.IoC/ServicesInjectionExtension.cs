using CustomerService.Applilcation.Email;
using CustomerService.Infrastructure.Common.Interfaces;
using CustomerService.Infrastructure.IoC.Setups;
using Microsoft.Extensions.DependencyInjection;
namespace CustomerService.Infrastructure.IoC;

public static class ServicesInjectionExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddDatabaseSetup();
        services.AddAutoMapperSetup();
        services.AddScoped<IEmailService, EmailService>(); 
    }
}

