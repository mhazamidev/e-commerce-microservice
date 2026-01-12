using IdentityService.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.API.DI;

public static class DatabaseSetup
{
    public static void AddDatabaseSetup(this IServiceCollection services)
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_Host");
        var dbPort = Environment.GetEnvironmentVariable("DB_Port");
        var dbName = Environment.GetEnvironmentVariable("DB_Name");
        var dbUser = Environment.GetEnvironmentVariable("DB_UserName");
        var dbPasssword = Environment.GetEnvironmentVariable("DB_Password");

        var connectionString = $"Server={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPasssword}";

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
    }
}
