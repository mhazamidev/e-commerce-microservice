using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Repositories;
using OrderService.Infrastructure.IoC.Setups;
using OrderService.Infrastructure.Persistence.Context;
using OrderService.Infrastructure.Persistence.Repositories;

namespace OrderService.Infrastructure.IoC;

public static class ServicesInjectionExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddAuthenticationSetup();
        services.AddSwaggerSetup();
        services.AddAutoMapperSetup();

        services.AddSingleton<IMongoContext, MongoContext>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}
