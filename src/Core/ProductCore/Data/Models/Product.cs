﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCore.Data.Models;

public class Product
{
    [Key] 
    public Guid Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
    public required string Name { get; init; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public required string Description { get; init; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 999999.99, ErrorMessage = "Price must be between 0 and 999999.99.")]
    public required decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "QuantityInStock cannot be negative.")]
    public required int QuantityInStock { get; set; }

    [Required] 
    public DateTime CreatedAt { get; set; }

    [Required] 
    public DateTime UpdatedAt { get; set; }
}