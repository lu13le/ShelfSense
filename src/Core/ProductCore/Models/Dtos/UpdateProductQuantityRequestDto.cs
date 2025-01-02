using System.ComponentModel.DataAnnotations;

namespace ProductCore.Models.Dtos;

public class UpdateProductQuantityRequestDto
{
    public required Guid Id { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer.")]
    public required int NewQuantity { get; set; }
}
