using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Product
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .Property(p => p.Name).HasMaxLength(100);

        modelBuilder.Entity<Product>()
            .Property(P => P.Description).HasMaxLength(500);

        // Configure Customer
        modelBuilder.Entity<Customer>()
            .Property(c => c.FirstName).HasMaxLength(50);

        modelBuilder.Entity<Customer>()
            .Property(c => c.LastName).HasMaxLength(50);

        modelBuilder.Entity<Customer>()
            .Property(c => c.Email).HasMaxLength(100);

        // Configure Order
        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(o => o.Status).HasMaxLength(20);

        modelBuilder.Entity<Order>()
            .Metadata.FindNavigation(nameof(Order.Items))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        // Configure OrderItem
        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.ProductName).HasMaxLength(100);
    }
}
