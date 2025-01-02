using OrderCore.Data.Models;
using OrderCore.Models.Dtos;

namespace OrderCore.Handlers.Interfaces;

public interface IOrderHandler
{
    Task<OrderDto?> GetById(Guid id);
}