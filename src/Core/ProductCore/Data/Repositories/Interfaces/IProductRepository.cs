using ProductCore.Data.Models;

namespace ProductCore.Data.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetById(Guid id);
    Task<IEnumerable<Product>> GetAll();
    Task<bool> Create(Product product);    
    Task<bool> Delete(Guid id);
    Task<bool> UpdatePrice(Guid id, decimal newPrice);
    Task<bool> UpdateQuantity(Guid id, int newQuantity);
}