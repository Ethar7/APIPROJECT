using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ECommerence.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerence.Persistence.Data.Configurations.ProductConfigurations
{
    public class ProductPrandConfig : IEntityTypeConfiguration<ProductPrand>
    {
        public void Configure(EntityTypeBuilder<ProductPrand> builder)
        {
            builder.Property(X => X.Name)
                .HasMaxLength(100);
        }
    }
}