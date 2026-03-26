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
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(p => p.Id)
                .IsUnique();

            entity.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(p => p.Description)
                .HasMaxLength(500);

            entity.Property(p => p.Price)
                .HasPrecision(18, 2);
        });

        // Configure Customer
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(c => c.Email)
                .IsUnique();

            entity.Property(c => c.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(c => c.LastName)
                .HasMaxLength(50);

            entity.Property(c => c.Email)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(c => c.PasswordHash)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(c => c.Role)
                .HasMaxLength(20)
                .HasDefaultValue("User")
                .IsRequired();
        });

        // Configure Order
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(o => o.Id)
                .IsUnique();

            entity.Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            entity.Property(o => o.Status)
                .HasMaxLength(20);

            entity.Metadata.FindNavigation(nameof(Order.Items))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        // Configure OrderItem
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasIndex(oi => oi.Id)
                .IsUnique();

            entity.Property(oi => oi.ProductName)
                .HasMaxLength(100);
            
            entity.Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);
        });
    }
}
