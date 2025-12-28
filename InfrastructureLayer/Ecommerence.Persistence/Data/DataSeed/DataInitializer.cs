// using System.Text.Json;
// using Ecommerence.Persistence.Data.DbContexts;
// using ECommerence.Domain.Contracts;
// using ECommerence.Domain.Entities;
// using ECommerence.Domain.Entities.ProductModule;
// using Microsoft.EntityFrameworkCore;

// namespace Ecommerence.Persistence.Data.DataSeed
// {
//     public class DataInitializer : IDataInitializer
//     {
//         private readonly StoreDbContext _dbContext;

//         public DataInitializer(StoreDbContext dbContext)
//         {
//             _dbContext = dbContext;
//         }
//         public void Initilize()
//         {
//             try
//             {
//                 var HasProducts = _dbContext.Products.Any();
//                 var HasBrands = _dbContext.ProductPrands.Any();
//                 var HasTypes = _dbContext.ProductTypes.Any();

//                 if (HasProducts && HasBrands && HasTypes) return;


//                 if (!HasBrands)
//                     SeedDataFromJson<ProductPrand, int>("brands.json", _dbContext.ProductPrands);

//                 if (!HasTypes)
//                      SeedDataFromJson<ProductType, int>("types.json", _dbContext.ProductTypes);
//                 if (!HasProducts)
//                       SeedDataFromJson<Product, int>("products.json", _dbContext.Products);

//                 _dbContext.SaveChanges();
//             }
//             catch (Exception ex)
//             {
                
//                 Console.WriteLine($"Data Seeding Failed : {ex}");
//             }
//         }

//         private void SeedDataFromJson<T, TKey>(string fileName , DbSet<T> dbset) where T : BaseEntity<TKey>
//         {
//             var filePath = @"..\Ecommerence.Persistence\Data\DataSeed\JSONFiles" + fileName;

//             if (!File.Exists(filePath)) throw new FileNotFoundException($"File {fileName} Not Found !");


//             try
//             {
//                 using var DataStream = File.OpenRead(filePath);

//                 var Data = JsonSerializer.Deserialize<List<T>>(DataStream, new JsonSerializerOptions()
//                 {
//                     PropertyNameCaseInsensitive = true,
//                 });

//                 if (Data is not null)
//                 {
//                     dbset.AddRange(Data);
//                 }
//             }
//             catch (Exception ex)
//             {
                
//                 Console.WriteLine($"Failed to Read Data From JSON : {ex}");
//             }

    
//     }
// }
// }


using System.Text.Json;
using Ecommerence.Persistence.Data.DbContexts;
using ECommerence.Domain.Contracts;
using ECommerence.Domain.Entities;
using ECommerence.Domain.Entities.IdentityModules;
using ECommerence.Domain.Entities.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerence.Persistence.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataInitializer(StoreDbContext dbContext, 
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
        )
        {
            _dbContext = dbContext;
            _userManager = userManager;
           _roleManager = roleManager;
        }

        public async Task InitilizeAsync()
        {
            try
            {
                
                var hasBrands = await _dbContext.ProductBrands.AnyAsync();
                var hasTypes = await _dbContext.ProductTypes.AnyAsync();
                var hasProducts = await _dbContext.Products.AnyAsync();

                if (hasBrands && hasTypes && hasProducts)
                {
                    Console.WriteLine("Database already seeded.");
                    return;
                }

                // Seed بالترتيب (Brands → Types → Products)
                if (!hasBrands)
                   await SeedFromJsonAsync<ProductBrand>("brands.json", _dbContext.ProductBrands);

                if (!hasTypes)
                    await SeedFromJsonAsync<ProductType>("types.json", _dbContext.ProductTypes);

                if (!hasProducts)
                   await SeedFromJsonAsync<Product>("products.json", _dbContext.Products);

                _dbContext.SaveChanges();
                Console.WriteLine("Data Seeding Completed Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data Seeding Failed: {ex}");
            }
        }

        private async Task SeedFromJsonAsync<T>(string fileName, DbSet<T> dbSet) where T : class
        {
            try
            {
                var filePath = GetJsonFilePath(fileName);

                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    return;
                }

                using var stream = File.OpenRead(filePath);
                var data = JsonSerializer.Deserialize<List<T>>(stream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (data != null && data.Count > 0)
                {
                    await dbSet.AddRangeAsync(data);
                    Console.WriteLine($"{data.Count} records added from {fileName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read {fileName}: {ex}");
            }
        }

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

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                
            if (!_roleManager.Roles.Any())
            {
               await _roleManager.CreateAsync(new IdentityRole("Admin"));
               await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if(!_userManager.Users.Any())
            {
                var Admin = new ApplicationUser()
                {
                    Email= "Ahmedmohamed@gmail.com",
                    DisplayName="Ahmedmohamed",
                    PhoneNumber="01065721210",
                    UserName = "AhmedMohamed"
                };
                
                var SuperAdmin = new ApplicationUser()
                {
                    Email= "nadasoliman@gmail.com",
                    DisplayName="NadaSoliman",
                    PhoneNumber="01012321210",
                    UserName = "NadaSoliman"
                };

                await _userManager.CreateAsync(Admin, "P@ssw0rd");
                await _userManager.CreateAsync(SuperAdmin, "P@ssw0rd");

                await _userManager.AddToRoleAsync(Admin, "Admin");
                await _userManager.AddToRoleAsync(SuperAdmin, "SuperAdmin");

            }
        }
            catch (System.Exception)
            {
                
                
            }
        }
    }
}

