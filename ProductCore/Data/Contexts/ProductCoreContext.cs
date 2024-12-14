using Microsoft.EntityFrameworkCore;
using ProductCore.Data.Models;

namespace ProductCore.Data.Contexts;

public class ProductCoreContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductCoreContext(DbContextOptions<ProductCoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Additional configurations for the Product model
        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        base.OnModelCreating(modelBuilder);
    }
}