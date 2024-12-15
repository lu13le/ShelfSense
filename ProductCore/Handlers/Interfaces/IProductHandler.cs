using ProductCore.Data.Models;

namespace ProductCore.Handlers.Interfaces;

public interface IProductHandler
{
    Task<Product?> GetById(Guid id);
}