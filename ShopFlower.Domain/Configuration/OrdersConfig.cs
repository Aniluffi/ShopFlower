using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopFlower.Data.Models;

namespace ShopFlower.Data.Configuration
{
    public class OrdersConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders").HasKey(c => c.Id);

            builder.HasMany(c => c.Products)
                .WithMany(c => c.Orders);

            builder.HasOne(c => c.User)
                .WithMany(c => c.Orders)
                .HasForeignKey(c => c.userId);
        }
    }
}
