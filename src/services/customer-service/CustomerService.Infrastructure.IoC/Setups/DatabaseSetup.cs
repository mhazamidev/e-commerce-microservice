using CustomerService.Infrastructure.Persistence.Context;
using CustomerService.Infrastructure.Persistence.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.IoC.Setups;

public static class DatabaseSetup
{
    public static void AddDatabaseSetup(this IServiceCollection service)
    {
        if (null == service)
            throw new ArgumentNullException(nameof(service));

        var db_host = Environment.GetEnvironmentVariable("DB_HOST");
        var db_name = Environment.GetEnvironmentVariable("DB_NAME");
        var db_password = Environment.GetEnvironmentVariable("DB_PASSWORD");
        var connectionString = $"Host={db_host};Database={db_name};User id=sa;Password={db_password};TrustServerCertificate=true;";
        service.AddDbContext<CustomerDbContext>((provider, options) =>
        {
            var interceptor = provider.GetRequiredService<AuditSaveChangesInterceptor>();
            options
                .UseSqlServer(connectionString)
                .AddInterceptors(interceptor);
        });
        service.AddSingleton<AuditSaveChangesInterceptor>();
    }
}
