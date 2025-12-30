using CustomerService.Applilcation.Email;
using CustomerService.Domain.Repositories;
using CustomerService.Infrastructure.Common.Interfaces;
using CustomerService.Infrastructure.IoC.Setups;
using CustomerService.Infrastructure.Persistence.Repositories;
using CustomerService.Infrastructure.Persistence.UOW;
using Microsoft.Extensions.DependencyInjection;
namespace CustomerService.Infrastructure.IoC;

public static class ServicesInjectionExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddDatabaseSetup();
        services.AddAutoMapperSetup();
        services.AddAuthenticationSetup();
        services.AddSwaggerSetup();

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICustomerUnitOfWok, CustomerUnitOfWok>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
    }
}

