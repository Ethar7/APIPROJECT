using Ecommerence.Persistence.Data.DbContexts;
using ECommerence.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerence.web.Extensions
{
    public static class WebAppRegisteration
    {
        public static async Task<WebApplication> MigrateDbAsync(this WebApplication app)
        {
            await using var Scope = app.Services.CreateAsyncScope();

            var dbContextService = Scope.ServiceProvider.GetRequiredService<StoreDbContext>();

            // if (dbContextService.Database.GetPendingMigrations().Any())

            //         dbContextService.Database.Migrate();

            var pendingMigrations = await dbContextService.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await dbContextService.Database.MigrateAsync();

            return app;
        }
    
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            await using var Scope = app.Services.CreateAsyncScope();
            var DataInitializerService = Scope.ServiceProvider.GetRequiredService<IDataInitializer>();

            await DataInitializerService.InitilizeAsync();

            return app;
        }
    }
}