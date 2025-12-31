using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Infrastructure.Persistence.Context;
using ProductService.Infrastructure.Persistence.Logging;

namespace ProductService.Infrastructure.IoC.Setups;

public static class DatabaseSetup
{
    public static void AddDatabaseSetup(this IServiceCollection service)
    {
        if (null == service)
            throw new ArgumentNullException(nameof(service));

        var db_host = Environment.GetEnvironmentVariable("DB_HOST");
        var db_name = Environment.GetEnvironmentVariable("DB_NAME");
        var db_password = Environment.GetEnvironmentVariable("DB_PASSWORD");
        var dbPort = Environment.GetEnvironmentVariable("DB_Port");
        var connectionString = $"server={db_host};port={dbPort};database={db_name};user=root;password={db_password}";
        service.AddDbContext<ProductDbContext>((provider, options) =>
        {
            var interceptor = provider.GetRequiredService<AuditSaveChangesInterceptor>();
            options
                .UseMySQL(connectionString)
                .AddInterceptors(interceptor);
        });
        service.AddSingleton<AuditSaveChangesInterceptor>();
    }
}
