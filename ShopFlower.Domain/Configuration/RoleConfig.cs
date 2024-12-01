using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopFlower.Data.Models;

namespace ShopFlower.Data.Configuration
{
    public class RoleConfig : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("Role").HasKey(c => c.Role);

            builder.HasMany(c => c.RolseUsers)
                .WithOne(c => c.Role)
                .HasForeignKey(c => c.RoleId);
        }
    }
}
