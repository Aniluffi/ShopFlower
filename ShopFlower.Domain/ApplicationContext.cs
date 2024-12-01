using Microsoft.EntityFrameworkCore;
using ShopFlower.Data.Configuration;
using ShopFlower.Data.Models;

namespace ShopFlower.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RolesUser> RolesUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Addresses> Addresss { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("MyDatabaseConnection"));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RolesUserConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new CartConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new WishListConfig());
        }
    }
}
