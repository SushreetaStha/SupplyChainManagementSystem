using Microsoft.EntityFrameworkCore;
using SupplyChainManagement.Helpers;
using SupplyChainManagement.Models;

namespace SupplyChainManagement.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seeding User Table
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "Admin",
                Username = "admin",
                Password = PasswordHelper.Hash("admin"),
                Role = Role.Admin
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Sushreeta",
                LastName = "Shrestha",
                Username = "sushreeta",
                Password = PasswordHelper.Hash("sushreeta"),
                Role = Role.Admin
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Ram",
                LastName = "Bohara",
                Username = "ram",
                Password = PasswordHelper.Hash("ram"),
                Role = Role.User
            });
        }
    }
}
