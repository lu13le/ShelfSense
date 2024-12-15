using ProductCore.Data.Models;

namespace ProductCore.Data.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetById(Guid id);
}