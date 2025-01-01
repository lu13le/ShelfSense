using ProductCore.Models.Dtos;

namespace ProductCore.Handlers.Interfaces;

public interface IProductHandler
{
    Task<ProductDto?> GetById(Guid id);
    Task<IEnumerable<ProductDto>> GetAll();
    Task<bool> Create(CreateProductRequestDto request);
    Task<bool> Delete(Guid id);
    Task<bool> UpdatePrice(UpdateProductPriceRequestDto request);
    Task<bool> UpdateQuantity(UpdateProductQuantityRequestDto request);
}