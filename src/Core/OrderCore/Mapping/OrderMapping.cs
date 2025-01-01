using OrderCore.Data.Models;
using OrderCore.Models.Dtos;

namespace OrderCore.Mapping;

public static class OrderMapping
{
    public static OrderDto ToOrderDto(this Order order) =>
        new()
        {
            Id = order.Id,
            CustomerEmail = order.CustomerEmail,
            CustomerName = order.CustomerName,
            OrderDate = order.OrderDate,
            OrderItems = order.OrderItems,
            ShippingAddress = order.ShippingAddress,
            State = order.State,
            TotalAmount = order.TotalAmount
        };
}