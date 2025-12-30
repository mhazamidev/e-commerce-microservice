using CustomerService.Applilcation.Core.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CustomerService.Infrastructure.IoC.Setups;

public static class AutoMapperSetup
{
    public static void AddAutoMapperSetup(this IServiceCollection services)
    {
        if (null == services)
            throw new ArgumentNullException(nameof(services));

        services.AddAutoMapper(config => { }, Assembly.GetAssembly(typeof(MapperBase)));
    }
}
