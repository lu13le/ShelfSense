namespace ProductCore.Models.Dtos;

public record ProductDto(Guid Id, string Name, string Description, decimal Price, int QuantityInStock, DateTime CreatedAt, DateTime UpdatedAt);
