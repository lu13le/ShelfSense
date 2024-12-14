using OrderCore.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderCore.Data.Models;

public class Order
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [EnumDataType(typeof(OrderState))]
    public OrderState State { get; set; }

    [Required] public DateTime OrderDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 999999.99, ErrorMessage = "TotalAmount must be between 0 and 999999.99.")]
    public decimal TotalAmount { get; set; }

    [StringLength(100, ErrorMessage = "CustomerName can't exceed 100 characters.")]
    public string CustomerName { get; set; }

    [StringLength(100, ErrorMessage = "CustomerEmail can't exceed 100 characters.")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string CustomerEmail { get; set; }

    [StringLength(250, ErrorMessage = "ShippingAddress can't exceed 250 characters.")]
    public string ShippingAddress { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];
}