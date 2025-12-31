using DomainLayer.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerence.Persistence.Data.Configurations
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.Property(oi=> oi.Price).HasColumnType("decimal(8,2)");

            builder.OwnsOne(o=>o.Product);
        }
    }
}