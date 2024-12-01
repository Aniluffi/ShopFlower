using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShopFlower.Data.Models;

namespace ShopFlower.Data.Configuration
{
    public class WishListConfig : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.ToTable("WishList").HasKey(s => s.Id);

            builder.HasOne(s => s.User)
              .WithMany(s => s.WishList)
              .HasForeignKey(s => s.userId);

            builder.HasMany(s => s.Products)
                .WithMany(s => s.WishList);

        }
    }
}
