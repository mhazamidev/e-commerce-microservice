using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IdentityService.API.DI;

public static class AuthenticationSetup
{
    public static void AddAuthenticationSetup(this IServiceCollection service, IConfiguration configuration)
    {
        if (null == service)
            throw new ArgumentNullException(nameof(service));

        var jwt_secret = Environment.GetEnvironmentVariable("JWT_SECRET");

        if (string.IsNullOrEmpty(jwt_secret))
            throw new InvalidOperationException("JWT Secret is not configured.");

        service.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            // JWT Setup
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt_secret)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = "ValidAt",
                ValidIssuer = "Issuer"
            };
        });
    }
}
