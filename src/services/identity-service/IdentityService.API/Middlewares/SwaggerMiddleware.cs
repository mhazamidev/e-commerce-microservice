namespace IdentityService.API.Middlewares;

public static class SwaggerMiddleware
{
    public static void UseSwaggerSetup(this IApplicationBuilder app)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        app.UseSwagger();
        app.UseSwaggerUI(option =>
        {
            option.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Commerce - API");
            option.OAuthAppName("E-Commerce API");
            option.OAuthClientId("interactive.public.short");
            option.OAuthUsePkce();
        });
    }
}
