using Carter;
using FluentValidation;
using IdentityService.API.DI;
using IdentityService.API.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddVersioning();
builder.Services.AddSwaggerSetup();
builder.Services.AddAuthenticationSetup(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddVersioning();
builder.Services.AddAuthorization();
builder.Services.AddDatabaseSetup();
builder.Services.AddDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerSetup();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCarter();
app.UseExceptionHandlerSetup();

app.Run();
