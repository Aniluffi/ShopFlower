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
                .WithMany(s => s.Products);

            builder.HasMany(s => s.Carts)
               .WithMany(s => s.Products);

            builder.HasMany(c => c.Orders)
                .WithMany(c => c.Products);
        }
    }
}
