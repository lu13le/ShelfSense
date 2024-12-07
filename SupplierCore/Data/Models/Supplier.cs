namespace SupplierCore.Data.Models;

public class Supplier
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ContactEmail { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string CreatedAt { get; set; }
}