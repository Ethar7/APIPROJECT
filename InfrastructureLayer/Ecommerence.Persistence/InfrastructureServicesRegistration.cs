using Ecommerence.Persistence.Data.DataSeed;
using Ecommerence.Persistence.Data.DbContexts;
using Ecommerence.Persistence.Data.Identity;
using Ecommerence.Persistence.Repositories;
using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities.IdentityModules;
using Microsoft.AspNetCore.Identity;
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

            services.AddDbContext<storeIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<storeIdentityDbContext>();

            services.AddScoped<IDataInitializer, DataInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
               return  ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString"));
            });

            return services;
        }
    }
};