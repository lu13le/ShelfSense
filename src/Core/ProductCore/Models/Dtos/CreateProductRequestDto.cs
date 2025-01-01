using System.ComponentModel.DataAnnotations;

namespace ProductCore.Models.Dtos;

public class CreateProductRequestDto
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock must be a non-negative integer.")]
    public int QuantityInStock { get; set; }
}