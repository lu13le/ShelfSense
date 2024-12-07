using Microsoft.EntityFrameworkCore;
using SupplierCore.Data.Models;

namespace SupplierCore.Data.Context;

public class SupplierCoreContext : DbContext
{
    public DbSet<Supplier> Suppliers { get; set; }

    public SupplierCoreContext(DbContextOptions<SupplierCoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Additional configurations for the Supplier model
        modelBuilder.Entity<Supplier>()
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        base.OnModelCreating(modelBuilder);
    }
}