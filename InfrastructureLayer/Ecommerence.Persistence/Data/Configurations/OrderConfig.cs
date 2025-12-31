using DomainLayer.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerence.Persistence.Data.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(o=>o.SubTotal).HasColumnType("decimal(8, 2)");

            builder.HasMany(o=>o.Items).WithOne();

            builder.HasOne(o=>o.DeliveryMethod)
            .WithMany()
            .HasForeignKey(o=>o.DeliveryMethodId);


            builder.OwnsOne(o=>o.Address);
            
        }
    }
}