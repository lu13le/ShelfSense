using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrderCore.Models.Enums;

namespace OrderCore.Models.Dtos;

public record CreateOrderRequestDto
{
    [Required]
    [EnumDataType(typeof(OrderState))]
    public required OrderState State { get; init; }

    [Required]
    public required DateTime OrderDate { get; init; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 999999.99, ErrorMessage = "TotalAmount must be between 0 and 999999.99.")]
    public required decimal TotalAmount { get; init; }

    [StringLength(100, ErrorMessage = "CustomerName can't exceed 100 characters.")]
    public required string CustomerName { get; init; }

    [StringLength(100, ErrorMessage = "CustomerEmail can't exceed 100 characters.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public required string CustomerEmail { get; init; }

    [StringLength(250, ErrorMessage = "ShippingAddress can't exceed 250 characters.")]
    public required string ShippingAddress { get; init; }

    public required List<OrderItemDto> OrderItems { get; init; } = [];
}
