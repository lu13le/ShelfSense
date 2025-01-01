using OrderCore.Data.Models;
using OrderCore.Data.Models.Enums;

namespace OrderCore.Models.Dtos;

public record OrderDto
{
    public Guid Id { get; init; }
    public OrderState State { get; init; }
    public DateTime OrderDate { get; init; }
    public decimal TotalAmount { get; init; }
    public string CustomerName { get; init; }
    public string CustomerEmail { get; init; }
    public string ShippingAddress { get; init; }
    public List<OrderItem> OrderItems { get; init; } = [];
}