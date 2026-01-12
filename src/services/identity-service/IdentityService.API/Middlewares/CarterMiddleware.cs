using Asp.Versioning;
using Asp.Versioning.Builder;
using Carter;

namespace IdentityService.API.Middlewares;

public static class CarterMiddleware
{
    public static void UseCarter(this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1))
                .ReportApiVersions()
                .Build();

        var rout = app.MapGroup("api/v{apiVersion:apiVersion}")
                    .WithApiVersionSet(apiVersionSet);

        rout.MapCarter();
    }
}
