using System.Text.Json;
using Ecommerence.Persistence.Data.DbContexts;
using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities.IdentityModules;
using ECommerence.Domain.Entities.ProductModule;
using DomainLayer.Models.OrderModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerence.Persistence.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataInitializer(
            StoreDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // ============================
        //  Application Data Seeding
        // ============================
        public async Task InitilizeAsync()
        {
            try
            {
                if (await _dbContext.ProductBrands.AnyAsync()
                    && await _dbContext.ProductTypes.AnyAsync()
                    && await _dbContext.Products.AnyAsync()
                    && await _dbContext.DeliveryMethods.AnyAsync())
                {
                    Console.WriteLine("Database already seeded.");
                    return;
                }

                await SeedFromJsonAsync<ProductBrand>("brands.json", _dbContext.ProductBrands);
                await SeedFromJsonAsync<ProductType>("types.json", _dbContext.ProductTypes);
                await SeedFromJsonAsync<Product>("products.json", _dbContext.Products);
                await SeedFromJsonAsync<DeliveryMethod>("delivery.json", _dbContext.DeliveryMethods);

                await _dbContext.SaveChangesAsync();
                Console.WriteLine("Application Data Seeding Completed Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application Data Seeding Failed: {ex}");
            }
        }

        // ============================
        //  Identity Data Seeding
        // ============================
        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!await _roleManager.Roles.AnyAsync())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!await _userManager.Users.AnyAsync())
                {
                    var admin = new ApplicationUser
                    {
                        Email = "Ahmedmohamed@gmail.com",
                        UserName = "AhmedMohamed",
                        DisplayName = "AhmedMohamed",
                        PhoneNumber = "01065721210"
                    };

                    var superAdmin = new ApplicationUser
                    {
                        Email = "nadasoliman@gmail.com",
                        UserName = "NadaSoliman",
                        DisplayName = "NadaSoliman",
                        PhoneNumber = "01012321210"
                    };

                    await _userManager.CreateAsync(admin, "P@ssw0rd");
                    await _userManager.CreateAsync(superAdmin, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(admin, "Admin");
                    await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                }

                Console.WriteLine("Identity Data Seeding Completed Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Identity Data Seeding Failed: {ex}");
            }
        }

        // ============================
        //  Generic JSON Seeder
        // ============================
        private async Task SeedFromJsonAsync<T>(string fileName, DbSet<T> dbSet)
            where T : class
        {
            if (await dbSet.AnyAsync())
                return;

            var filePath = GetJsonFilePath(fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }

            var json = await File.ReadAllTextAsync(filePath);

            var data = JsonSerializer.Deserialize<List<T>>(json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (data is not null && data.Any())
            {
                await dbSet.AddRangeAsync(data);
                Console.WriteLine($"{data.Count} records added from {fileName}");
            }
        }

        // ============================
        //  JSON Path Resolver (Linux Safe)
        // ============================
        private string GetJsonFilePath(string fileName)
        {
            var baseDir = AppContext.BaseDirectory;

            var filePath = Path.Combine(
                baseDir,
                "..", "..", "..", "..", "..",
                "InfrastructureLayer",
                "Ecommerence.Persistence",
                "Data",
                "DataSeed",
                "JSONFiles",
                fileName
            );

            return Path.GetFullPath(filePath);
        }
    }
}

