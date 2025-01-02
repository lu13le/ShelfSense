using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderCore.Data.Contexts;
using OrderCore.Data.Models;
using OrderCore.Data.Repositories.Interfaces;

namespace OrderCore.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderCoreContext _orderCoreContext;
    private readonly ILogger<OrderRepository> _logger;

    public OrderRepository(OrderCoreContext orderCoreContext, ILogger<OrderRepository> logger)
    {
        _orderCoreContext = orderCoreContext ?? throw new ArgumentNullException(nameof(orderCoreContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Order?> GetById(Guid id)
    {
        try
        {
            var order = await _orderCoreContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            LogOrderFetch(id, order);
            return order;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An unexpected error occurred while fetching the order with ID: {id}");
            throw;
        }
    }

    public async Task<bool> Create(Order order)
    {
        try
        {
            _orderCoreContext.Add(order);
            await _orderCoreContext.SaveChangesAsync();
            _logger.LogInformation("Successfully created order with ID: {Id}", order.Id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating an order.");
            return false;
        }
    }

    private void LogOrderFetch(Guid id, Order? order)
    {
        _logger.LogInformation(order is null
            ? $"Order with ID: {id} not found"
            : $"Order with ID: {id} fetched successfully", id);
    }
}