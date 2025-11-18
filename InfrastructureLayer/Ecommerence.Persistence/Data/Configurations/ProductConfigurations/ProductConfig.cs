using ECommerence.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerence.Persistence.Data.Configurations.ProductConfigurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(X => X.Name)
            .HasMaxLength(100);
            builder.Property(X => X.Description)
            .HasMaxLength(500);
            builder.Property(X => X.PictureUrl)
            .HasMaxLength(200);

            builder.Property(X => X.Price)
            .HasPrecision(18, 2);

            builder.HasOne(X => X.ProductPrands)
            .WithMany()
            .HasForeignKey(X => X.BrandId);

            builder.HasOne(X => X.ProductTypes)
            .WithMany()
            .HasForeignKey(X => X.TypeId);
        }
    }

}