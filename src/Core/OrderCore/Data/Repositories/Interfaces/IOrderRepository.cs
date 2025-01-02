using OrderCore.Data.Models;

namespace OrderCore.Data.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetById(Guid id);
    Task<bool> Create(Order order);
}