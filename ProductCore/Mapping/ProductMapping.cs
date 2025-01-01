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

    public static Product ToProduct(this CreateProductRequestDto request)
    {
        if (request.Price < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(request.Price));

        if (request.QuantityInStock < 0)
            throw new ArgumentException("Quantity in stock cannot be negative.", nameof(request.QuantityInStock));

        return new Product
        {
            Description = request.Description,
            Name = request.Name,
            Price = request.Price,
            QuantityInStock = request.QuantityInStock,
        };
    }
}