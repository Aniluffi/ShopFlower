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
                .HasMany(s => s.Carts)
                 .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(c => c.userId);

            builder.HasMany(j => j.WishList)
                .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(c => c.userId);

            builder.HasMany(s => s.Roles)
                .WithOne(s => s.User)
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(s => s.UserId);

            builder.HasOne(c => c.Addresses)
                .WithMany(c => c.Users)
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(c => c.AddressesId);

            builder.HasMany(j => j.Orders)
                .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(c => c.userId);
        }
    }
}
