using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace IdentityService.API.Middlewares;

public static class ExceptionHandlerMiddleware
{
    public static void UseExceptionHandlerSetup(this IApplicationBuilder app)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async httpContext =>
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
                var pds = httpContext.RequestServices.GetService<IProblemDetailsService>();


                if (pds == null || !await pds.TryWriteAsync(new() { HttpContext = httpContext }))
                {
                    if (contextFeature?.Error.GetType() == typeof(ValidationException))
                        await httpContext.Response.WriteAsJsonAsync(new
                        {
                            success = false,
                            statusCode = (int)HttpStatusCode.BadRequest,
                            message = contextFeature.Error.Message
                        });
                    else
                        await httpContext.Response.WriteAsJsonAsync(new
                        {
                            success = false,
                            statusCode = (int)HttpStatusCode.InternalServerError,
                            message = "Internal Server Error"
                        });
                }
            });
        });

    }
}
