using CustomerService.Infrastructure.IoC;
using CustomerService.Infrastructure.IoC.Setups;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.RegisterServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerSetup();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
