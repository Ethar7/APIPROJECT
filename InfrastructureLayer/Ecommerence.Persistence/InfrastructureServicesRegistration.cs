using Ecommerence.Persistence.Data.DataSeed;
using Ecommerence.Persistence.Data.DbContexts;
using Ecommerence.Persistence.Repositories;
using ECommerence.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Ecommerence.Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IDataInitializer, DataInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
               return  ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString"));
            });

            return services;
        }
    }
};