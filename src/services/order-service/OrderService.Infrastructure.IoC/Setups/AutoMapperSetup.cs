using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Core.AutoMapper;
using System.Reflection;

namespace OrderService.Infrastructure.IoC.Setups;

public static class AutoMapperSetup
{
    public static void AddAutoMapperSetup(this IServiceCollection services)
    {
        if (null == services)
            throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(config => { }, Assembly.GetAssembly(typeof(MapperBase)));
    }
}
