using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.IoC.Setups;
using ProductService.Infrastructure.Persistence.Repositories;
using ProductService.Infrastructure.Persistence.UOW;
namespace ProductService.Infrastructure.IoC;

public static class ServicesInjectionExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddDatabaseSetup();
        services.AddAutoMapperSetup();
        services.AddAuthenticationSetup();
        services.AddSwaggerSetup();

        services.AddScoped<IProductUnitOfWok, ProductUnitOfWok>();
        services.AddScoped<IProductRepository, ProductRepository>();

    }
}

