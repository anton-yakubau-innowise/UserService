using Microsoft.EntityFrameworkCore;
//using UserService.API.Middleware;
using UserService.Application;
using UserService.Infrastructure;
using UserService.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();   

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();