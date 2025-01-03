﻿using System.ComponentModel.DataAnnotations;

namespace ProductCore.Models.Dtos;

public class UpdateProductPriceRequestDto
{
    public required Guid Id { get; set; }

    [Range(0.01, 999999, ErrorMessage = "Price must be greater than zero and less than 999,999.")]
    public required decimal NewPrice { get; set; }
}
