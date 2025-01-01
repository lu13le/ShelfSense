using OrderCore.Data.Models;
using OrderCore.Data.Repositories.Interfaces;
using OrderCore.Handlers.Interfaces;

namespace OrderCore.Handlers;

public class OrderHandler : IOrderHandler
{
    private readonly IOrderRepository _orderRepository;

    public OrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<Order?> GetById(Guid id)
    {
        var order = await _orderRepository.GetById(id);
        return order;
    }
}