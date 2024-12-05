using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopFlower.Data.Models;

namespace ShopFlower.Data.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product").HasKey(s => s.Id);

            builder.HasMany(s => s.WishList)
                .WithOne(s => s.Products)
                .HasForeignKey(s => s.ProductId);

            builder.HasMany(s => s.Carts)
               .WithOne(s => s.Products)
               .HasForeignKey(s => s.ProductId);

            builder.HasMany(c => c.Orders)
                .WithOne(c => c.Products)
                .HasForeignKey(c => c.ProductId);
        }
    }
}
