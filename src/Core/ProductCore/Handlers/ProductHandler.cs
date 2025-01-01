using ProductCore.Data.Repositories.Interfaces;
using ProductCore.Handlers.Interfaces;
using ProductCore.Mapping;
using ProductCore.Models.Dtos;

namespace ProductCore.Handlers;

public class ProductHandler : IProductHandler
{
    private readonly IProductRepository _productRepository;

    public ProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ProductDto?> GetById(Guid id)
    {
        var product = await _productRepository.GetById(id);
        return product?.ToProductDto();
    }

    public async Task<IEnumerable<ProductDto>> GetAll()
    {
        var products = await _productRepository.GetAll();
        return products.Select(product => product.ToProductDto());
    }

    public async Task<bool> Create(CreateProductRequestDto request) =>
        await _productRepository.Create(request.ToProduct());

    public async Task<bool> Delete(Guid id) =>
        await _productRepository.Delete(id);

    public async Task<bool> UpdatePrice(UpdateProductPriceRequestDto request) =>
        await _productRepository.UpdatePrice(request.Id, request.NewPrice);

    public async Task<bool> UpdateQuantity(UpdateProductQuantityRequestDto request) =>
        await _productRepository.UpdateQuantity(request.Id, request.NewQuantity);
}