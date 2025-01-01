using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportingCore.Data.Models;

public class SalesReport
{
    [Key] 
    public Guid Id { get; set; }

    [Required] public DateTime ReportDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 999999999.99, ErrorMessage = "TotalSalesAmount must be between 0 and 999999999.99.")]
    public decimal TotalSalesAmount { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "TotalOrders cannot be negative.")]
    public int TotalOrders { get; set; }

    [StringLength(100, ErrorMessage = "GeneratedBy cannot exceed 100 characters.")]
    public string GeneratedBy { get; set; }
}