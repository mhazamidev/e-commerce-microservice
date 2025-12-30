using CustomerService.Applilcation.Core.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CustomerService.Infrastructure.IoC.Setups;

public static class AutoMapperSetup
{
    public static void AddAutoMapperSetup(this IServiceCollection services)
    {
        services.AddAutoMapper(config => { }, Assembly.GetAssembly(typeof(MapperBase)));
    }
}
