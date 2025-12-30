using CustomerService.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.RegisterServices();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
