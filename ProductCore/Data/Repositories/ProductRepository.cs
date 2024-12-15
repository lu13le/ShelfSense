using Microsoft.EntityFrameworkCore;
using ProductCore.Data.Contexts;
using ProductCore.Data.Models;
using ProductCore.Data.Repositories.Interfaces;

namespace ProductCore.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductCoreContext _productCoreContext;

    public ProductRepository(ProductCoreContext productCoreContext)
    {
        _productCoreContext = productCoreContext ?? throw new ArgumentNullException(nameof(productCoreContext));
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _productCoreContext.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
}