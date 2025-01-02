using OrderCore.Data.Models;
using OrderCore.Models.Dtos;

namespace OrderCore.Mapping;

public static class OrderMapping
{
    public static OrderDto ToOrderDto(this Order order) =>
        new(
            order.Id,
            order.State.ToDtoOrderState(),
            order.OrderDate,
            order.TotalAmount,
            order.CustomerName,
            order.CustomerEmail,
            order.ShippingAddress,
            order.OrderItems.Select(item => item.ToOrderItemDto()).ToList()
        );

    private static Models.Enums.OrderState ToDtoOrderState(this Data.Models.Enums.OrderState state) =>
        state switch
        {
            Data.Models.Enums.OrderState.Pending => Models.Enums.OrderState.Pending,
            Data.Models.Enums.OrderState.Processing => Models.Enums.OrderState.Processing,
            Data.Models.Enums.OrderState.Shipped => Models.Enums.OrderState.Shipped,
            Data.Models.Enums.OrderState.Canceled => Models.Enums.OrderState.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(state), $"Unhandled state: {state}")
        };

    private static Data.Models.Enums.OrderState ToDataOrderState(this Models.Enums.OrderState state) =>
        state switch
        {
            Models.Enums.OrderState.Pending => Data.Models.Enums.OrderState.Pending,
            Models.Enums.OrderState.Processing => Data.Models.Enums.OrderState.Processing,
            Models.Enums.OrderState.Shipped => Data.Models.Enums.OrderState.Shipped,
            Models.Enums.OrderState.Canceled => Data.Models.Enums.OrderState.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(state), $"Unhandled state: {state}")
        };

    public static Order ToOrder(this CreateOrderRequestDto dto) =>
        new Order
        {
            Id = Guid.NewGuid(),
            State = dto.State.ToDataOrderState(),
            OrderDate = dto.OrderDate,
            TotalAmount = dto.TotalAmount,
            CustomerName = dto.CustomerName,
            CustomerEmail = dto.CustomerEmail,
            ShippingAddress = dto.ShippingAddress,
            OrderItems = dto.OrderItems.Select(item => item.ToOrderItem()).ToList()
        };

    private static OrderItemDto ToOrderItemDto(this OrderItem orderItem) =>
        new()
        {
            OrderId = orderItem.OrderId,
            PriceAtPurchase = orderItem.PriceAtPurchase,
            ProductId = orderItem.ProductId,
            Quantity = orderItem.Quantity
        };

    private static OrderItem ToOrderItem(this OrderItemDto dto) =>
        new()
        {
            Id = new Guid(),
            OrderId = dto.OrderId,
            PriceAtPurchase = dto.PriceAtPurchase,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity
        };
}