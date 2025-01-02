using System.ComponentModel.DataAnnotations;

namespace OrderCore.Models.Dtos;

public class OrderItemDto
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Order item quantity cannot be negative number.")]  
    public int Quantity { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Order item price cannot be negative number.")]  
    public decimal PriceAtPurchase { get; set; }
}