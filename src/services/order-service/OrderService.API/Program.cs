using OrderService.Infrastructure.IoC;
using OrderService.Infrastructure.IoC.Setups;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.RegisterServices();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwaggerSetup();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
