using OrderCore.Data.Models;

namespace OrderCore.Handlers.Interfaces;

public interface IOrderHandler
{
    Task<Order?> GetById(Guid id);
}