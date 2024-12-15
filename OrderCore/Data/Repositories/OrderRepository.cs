using Microsoft.EntityFrameworkCore;
using OrderCore.Data.Contexts;
using OrderCore.Data.Models;
using OrderCore.Data.Repositories.Interfaces;

namespace OrderCore.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderCoreContext _orderCoreContext;

    public OrderRepository(OrderCoreContext orderCoreContext)
    {
        _orderCoreContext = orderCoreContext ?? throw new ArgumentNullException(nameof(orderCoreContext));
    }

    public async Task<Order?> GetById(Guid id)
    {
        var order = await _orderCoreContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        return order;
    }
}