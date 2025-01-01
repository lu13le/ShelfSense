using System.ComponentModel.DataAnnotations;

namespace ProductCore.Models.Dtos;

public class UpdateProductQuantityRequestDto
{
    public Guid Id { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer.")]
    public int NewQuantity { get; set; }
}
