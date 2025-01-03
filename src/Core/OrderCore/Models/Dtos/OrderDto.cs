﻿using OrderCore.Models.Enums;

namespace OrderCore.Models.Dtos;

public record OrderDto(
    Guid Id,
    OrderState State,
    DateTime OrderDate,
    decimal TotalAmount,
    string CustomerName,
    string CustomerEmail,
    string ShippingAddress,
    List<OrderItemDto> OrderItems);