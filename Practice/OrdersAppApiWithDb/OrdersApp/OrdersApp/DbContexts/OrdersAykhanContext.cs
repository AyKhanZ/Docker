using Microsoft.EntityFrameworkCore;
using OrdersApp.Models;

namespace OrdersApp.DbContexts;
public class OrdersAykhanContext : DbContext
{
    public OrdersAykhanContext(DbContextOptions<OrdersAykhanContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
     
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId);

        modelBuilder.Entity<OrderDetails>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);

        modelBuilder.Entity<Image>()
          .HasOne(i => i.Product)
          .WithMany(p => p.Images)
          .HasForeignKey(i => i.ProductId);
    }
}