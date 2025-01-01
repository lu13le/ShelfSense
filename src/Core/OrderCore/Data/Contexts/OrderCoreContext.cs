using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderCore.Data.Models;
using OrderCore.Data.Models.Enums;

namespace OrderCore.Data.Contexts;

public class OrderCoreContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public OrderCoreContext(DbContextOptions<OrderCoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relationships and constraints
        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => new { oi.OrderId, oi.ProductId });

        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);
        
        var converter = new EnumToStringConverter<OrderState>();
        modelBuilder.Entity<Order>()
            .Property(o => o.State)
            .HasConversion(converter);

        base.OnModelCreating(modelBuilder);
    }
}