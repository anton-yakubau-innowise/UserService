using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Interfaces;
using UserService.Domain.Repositories;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Persistence.Repositories;

namespace UserService.Infrastructure;

// public static class InfrastructureServiceExtensions
// {
//     public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
//     {
//         services.AddDbContext<UserDbContext>(options =>
//             options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

//         services.AddScoped<IVehicleRepository, VehicleRepository>();
//         services.AddScoped<IUnitOfWork, UnitOfWork>();

//         return services;
//     }
// }