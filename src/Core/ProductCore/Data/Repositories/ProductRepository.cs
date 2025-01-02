using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductCore.Data.Contexts;
using ProductCore.Data.Models;
using ProductCore.Data.Repositories.Interfaces;

namespace ProductCore.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductCoreContext _productCoreContext;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(ProductCoreContext productCoreContext, ILogger<ProductRepository> logger)
    {
        _productCoreContext = productCoreContext ?? throw new ArgumentNullException(nameof(productCoreContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Product?> GetById(Guid id)
    {
        try
        {
            var product = await _productCoreContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            LogProductFetch(id, product);
            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An unexpected error occurred while fetching the product with ID: {id}");
            throw;
        }
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        try
        {
            var products = await _productCoreContext.Products.ToListAsync();
            _logger.LogInformation("Successfully fetched {Count} products from the database.", products.Count);
            return products;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching all products.");
            throw;
        }
    }

    public async Task<bool> Create(Product product)
    {
        try
        {
            product.Id = Guid.NewGuid();
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            _productCoreContext.Add(product);
            await _productCoreContext.SaveChangesAsync();
            _logger.LogInformation("Successfully created product with ID: {Id} and name: {ProductName}", product.Id,
                product.Name);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating the product.");
            return false;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            var product = await _productCoreContext.Products
                .FirstOrDefaultAsync(p => p.Id == id);
            
            LogProductFetch(id, product);

            if (product is null)
            {
                return false;
            }
           
            _productCoreContext.Products.Remove(product);
            await _productCoreContext.SaveChangesAsync();

            _logger.LogInformation("Successfully removed product with ID: {Id} and name: {ProductName}", product.Id, product.Name);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while deleting the product with id: {Id}.", id);
            return false;
        }
    }

    public async Task<bool> UpdatePrice(Guid id, decimal newPrice)
    {
        try
        {
            var product = await _productCoreContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            LogProductFetch(id, product);

            if (product is null)
            {
                return false;
            }

            product.Price = newPrice;
            await _productCoreContext.SaveChangesAsync();
            _logger.LogInformation("Successfully update product price with product ID: {Id} and name: {ProductName}",
                product.Id, product.Name);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while updating the product price.");
            return false;
        }
    }

    public async Task<bool> UpdateQuantity(Guid id, int newQuantity)
    {
        try
        {
            var product = await _productCoreContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            LogProductFetch(id, product);

            if (product is null)
            {
                return false;
            }

            product.QuantityInStock = newQuantity;
            await _productCoreContext.SaveChangesAsync();
            _logger.LogInformation("Successfully update product quantity with product ID: {Id} and name: {ProductName}",
                product.Id, product.Name);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while updating the product quantity.");
            return false;
        }
    }
    
    private void LogProductFetch(Guid id, Product? product)
    {
        _logger.LogInformation(product is null
            ? $"Product with ID: {id} not found"
            : $"Product with ID: {id} fetched successfully", id);
    }
}