using OrderCore.Models.Dtos;

namespace OrderCore.Handlers.Interfaces;

public interface IOrderHandler
{
    Task<OrderDto?> GetById(Guid id);
    Task<bool> Create(CreateOrderRequestDto request);
}