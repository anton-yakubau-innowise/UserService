using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using UserService.Domain.Entities;
using UserService.Domain.Repositories;
using UserService.Infrastructure.Persistence.Configuration;
using UserService.Infrastructure.Persistence.Repositories;

namespace UserService.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbSettings = new MongoDbSettings();
            configuration.GetSection(MongoDbSettings.SectionName).Bind(mongoDbSettings);

            if (string.IsNullOrEmpty(mongoDbSettings.ConnectionString) || string.IsNullOrEmpty(mongoDbSettings.DatabaseName))
            {
                throw new InvalidOperationException("ConnectionString or DatabaseName for MongoDB are not set in the configuration.");
            }

            services.AddIdentity<User, MongoIdentityRole<Guid>>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddMongoDbStores<User, MongoIdentityRole<Guid>, Guid>(mongoDbSettings.ConnectionString, mongoDbSettings.DatabaseName)
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoDbSettings.ConnectionString));
            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(mongoDbSettings.DatabaseName);
            });

            services.AddScoped<IUserRepository, MongoUserRepository>();
            
            return services;
        }
    }
}
