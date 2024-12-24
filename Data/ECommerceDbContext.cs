using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Models;

namespace ECommerceAPI.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18, 2)"); 

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)"); 
        }
    }
}
