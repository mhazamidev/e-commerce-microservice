using IdentityService.API.Domain.Repositories;
using IdentityService.API.DTO;
using IdentityService.API.Persistence.Repositories;

namespace IdentityService.API.DI;

public static class JwtSetup
{
    public static void AddJwtRepository(this IServiceCollection services)
    {
        var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET");
        var jwtIssuer = Environment.GetEnvironmentVariable("JWT_Issuer");
        var jwtAudience = Environment.GetEnvironmentVariable("JWT_Audience");
        var jwtExpiration = Environment.GetEnvironmentVariable("JWT_Expiration");


        services.Configure<JwtOption>(options =>
        {
            options.Issuer = jwtIssuer;
            options.Audience = jwtAudience;
            options.Key = jwtKey;
            options.ExpirationInMinute = int.Parse(jwtExpiration);
        });

        services.AddScoped<IJwtRepository, JwtRepository>();
    }
}
