using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopFlower.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopFlower.Data.Configuration
{
    public class RolesUserConfig : IEntityTypeConfiguration<RolesUser>
    {
        public void Configure(EntityTypeBuilder<RolesUser> builder)
        {
            //builder.ToTable("RolesUser");

            builder.HasKey(t => new {t.RoleId, t.UserId});

            builder.HasOne(s => s.Role)
                .WithMany(s => s.RolseUsers)
                .HasForeignKey(s => s.RoleId);

            builder.HasOne(s => s.User)
                .WithMany(s => s.Roles)
                .HasForeignKey(s => s.UserId);
        }
    }
}
