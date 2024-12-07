using Microsoft.EntityFrameworkCore;
using ReportingCore.Data.Models;

namespace ReportingCore.Data.Context;

public class ReportingCoreContext : DbContext
{
    public DbSet<SalesReport> SalesReports { get; set; }

    public ReportingCoreContext(DbContextOptions<ReportingCoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Additional configurations for the SalesReport model
        modelBuilder.Entity<SalesReport>()
            .Property(r => r.ReportDate)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}