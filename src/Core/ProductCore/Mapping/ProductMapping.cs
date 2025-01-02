using ProductCore.Data.Models;
using ProductCore.Models.Dtos;

namespace ProductCore.Mapping;

public static class ProductMapping
{
    public static ProductDto ToProductDto(this Product product)
    {
        var productDto = new ProductDto(product.Id, product.Name, product.Description, product.Price,
            product.QuantityInStock, product.CreatedAt, product.UpdatedAt);
        
        _ = (productDto.Id, productDto.Name, productDto.Description, productDto.Price, productDto.QuantityInStock,
            productDto.CreatedAt, productDto.UpdatedAt);

        return productDto;
    }

    public static Product ToProduct(this CreateProductRequestDto request)
    {
        return new Product
        {
            Description = request.Description,
            Name = request.Name,
            Price = request.Price,
            QuantityInStock = request.QuantityInStock,
        };
    }
}