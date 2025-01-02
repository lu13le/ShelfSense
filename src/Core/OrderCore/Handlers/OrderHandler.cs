using OrderCore.Data.Repositories.Interfaces;
using OrderCore.Handlers.Interfaces;
using OrderCore.Mapping;
using OrderCore.Models.Dtos;

namespace OrderCore.Handlers;

public class OrderHandler : IOrderHandler
{
    private readonly IOrderRepository _orderRepository;

    public OrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<OrderDto?> GetById(Guid id)
    {
        var order = await _orderRepository.GetById(id);
        return order?.ToOrderDto();
    }

    public async Task<bool> Create(CreateOrderRequestDto request)
        => await _orderRepository.Create(request.ToOrder());
}