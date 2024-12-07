using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopFlower.Data.Models;

namespace ShopFlower.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User").HasKey(t => t.Id);


            builder
                .HasOne(c => c.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<User>(c => c.CartId)
                .OnDelete(DeleteBehavior.Cascade);



            builder.HasOne(j => j.WishList)
                .WithOne(c => c.User)
              .HasForeignKey<User>(c => c.WishListId)
                .OnDelete(DeleteBehavior.Cascade);



            builder.HasMany(s => s.Roles)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(c => c.Addresses)
                .WithMany(c => c.Users)
                .HasForeignKey(c => c.AddressesId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(j => j.Orders)
                .WithOne(c => c.User)
              .HasForeignKey<User>(c => c.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
