namespace ReportingCore.Data.Models;

public class SalesReport
{
    public Guid Id { get; set; }
    public DateTime ReportDate { get; set; }
    public decimal TotalSalesAmount { get; set; }
    public int TotalOrders { get; set; }
    public string GeneratedBy { get; set; }
}