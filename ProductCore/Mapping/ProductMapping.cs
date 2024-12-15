using ProductCore.Data.Models;
using ProductCore.Models.Dtos;

namespace ProductCore.Mapping;

public static class ProductMapping
{
    public static ProductDto ToProductDto(this Product product) =>
        new()
        {
            Id = product.Id,
            CreatedAt = product.CreatedAt,
            Description = product.Description,
            Name = product.Name,
            Price = product.Price,
            QuantityInStock = product.QuantityInStock,
            UpdatedAt = product.UpdatedAt
        };
}