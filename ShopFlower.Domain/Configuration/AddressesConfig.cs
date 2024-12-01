using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopFlower.Data.Models;

namespace ShopFlower.Data.Configuration
{
    public class AddressesConfig : IEntityTypeConfiguration<Addresses>
    {
        public void Configure(EntityTypeBuilder<Addresses> builder)
        {
            builder.ToTable("Addresses").HasKey(c => c.Id);

            builder.HasMany(c => c.Users)
                .WithOne(c => c.Addresses)
                .HasForeignKey(c => c.AddressesId);
        }
    }
}
