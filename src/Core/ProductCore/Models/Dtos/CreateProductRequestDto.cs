using System.ComponentModel.DataAnnotations;

namespace ProductCore.Models.Dtos;

public class CreateProductRequestDto
{
    [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
    public required string Name { get; set; }
    
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public required string Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public required decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock must be a non-negative integer.")]
    public required int QuantityInStock { get; set; }
}