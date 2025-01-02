using OrderCore.Data.Models;
using OrderCore.Models.Dtos;

namespace OrderCore.Mapping;

public static class OrderMapping
{
    public static OrderDto ToOrderDto(this Order order) =>
        new(
            order.Id,
            order.State,
            order.OrderDate,
            order.TotalAmount,
            order.CustomerName,
            order.CustomerEmail,
            order.ShippingAddress,
            order.OrderItems
        );
}