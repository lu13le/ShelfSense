﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OrderCore.Handlers.Interfaces;
using OrderCore.Mapping;

namespace OrderCore.Endpoints;

public static class Orders
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder routes, string prefix)
    {
        var orders = routes.MapGroup($"{prefix}/Orders").WithTags("Orders");

        orders.MapGet("/GetById", async ([FromQuery] Guid id,
            [FromServices] IOrderHandler handler) =>
        {
            var order = await handler.GetById(id);
            return order is null ? Results.NotFound("No order for given id found.") : Results.Ok(order.ToOrderDto());
        });
    }
}