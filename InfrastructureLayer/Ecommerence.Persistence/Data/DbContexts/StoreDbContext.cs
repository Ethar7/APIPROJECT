using System.Reflection;
using ECommerence.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Models.OrderModels;

namespace Ecommerence.Persistence.Data.DbContexts
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly()
            );
        }

        // =======================
        // Product Module Tables
        // =======================
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        // =======================
        // Order Module Tables
        // =======================
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    }
}
