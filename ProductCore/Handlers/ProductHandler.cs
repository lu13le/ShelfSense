using ProductCore.Data.Models;
using ProductCore.Data.Repositories.Interfaces;
using ProductCore.Handlers.Interfaces;

namespace ProductCore.Handlers;

public class ProductHandler : IProductHandler
{
    private readonly IProductRepository _productRepository;

    public ProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _productRepository.GetById(id);
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _productRepository.GetAll();
    }
}