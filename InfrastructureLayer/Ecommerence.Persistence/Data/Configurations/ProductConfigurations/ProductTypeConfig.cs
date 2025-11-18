using Microsoft.EntityFrameworkCore;

using ECommerence.Domain.Entities.ProductModule;
namespace Ecommerence.Persistence.Data.Configurations.ProductConfigurations

{
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductTypeConfig : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.Property(X => X.Name)
            .HasMaxLength(100);
    }
}
}