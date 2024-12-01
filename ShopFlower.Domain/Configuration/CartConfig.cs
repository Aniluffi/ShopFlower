using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopFlower.Data.Models;

namespace ShopFlower.Data.Configuration
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart").HasKey(s => s.Id);

            builder.HasOne(s => s.User)
                .WithMany(s => s.Carts)
                .HasForeignKey(s => s.userId);

            builder.HasMany(s => s.Products)
                .WithMany(s => s.Carts);

        }
    }
}
