// using System.Reflection;
// using ECommerence.Domain.Entities.ProductModule;
// using Microsoft.EntityFrameworkCore;

// namespace  Ecommerence.Persistence.Data.DbContexts
// {
//     public class StoreDbContext : DbContext
//     {
//         public StoreDbContext(DbContextOptions<StoreDbContext> options): base(options)
//         {
            
//         }

//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
//         }

//         // Tables

//         public DbSet<Product> Products{get; set;}

//         public DbSet<ProductPrand> ProductPrands {get; set;}

//         public DbSet <ProductType> ProductTypes {get; set;}
//     }
// }


using System.Reflection;
using ECommerence.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;

namespace Ecommerence.Persistence.Data.DbContexts
{
    public class StoreDbContext : DbContext
    {
        
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        // جداول (DbSets)
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrand> ProductPrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
    }
}
