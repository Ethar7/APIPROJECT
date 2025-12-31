using DomainLayer.Models.OrderModels;
using Microsoft.EntityFrameworkCore;

namespace Ecommerence.Persistence.Data.Configurations
{
    public class DeleveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");
            builder.Property(d=>d.Price).HasColumnType("decimal(8,2)");
            builder.Property(d=>d.ShortName).HasColumnType("varchar(200)");
            builder.Property(d=>d.Description).HasColumnType("varchar(500)");
            builder.Property(d=>d.DeliveryTime).HasColumnType("varchar(200)");
        }
    }
}