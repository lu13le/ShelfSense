using OrderCore.Data.Models.Enums;

namespace OrderCore.Data.Models;

public class Order
{
    public Guid Id { get; set; }
    public OrderState State { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string ShippingAddress { get; set; }
    
    public List<OrderItem> OrderItems { get; set; } = new();
}