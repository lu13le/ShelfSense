using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OrderCore.Extensions;
using OrderCore.Handlers.Interfaces;
using OrderCore.Models.Dtos;

namespace OrderCore.Endpoints;

public static class Orders
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder routes, string prefix)
    {
        var orders = routes.MapGroup($"{prefix}/Orders").WithTags("Orders");

        MapGetEndpoints(orders);
        MapPostEndpoints(orders);
    }

    private static void MapGetEndpoints(RouteGroupBuilder orders)
    {
        orders.MapGet("/GetById", async ([FromQuery] Guid id,
                [FromServices] IOrderHandler handler) =>
            {
                var order = await handler.GetById(id);
                return order is null ? Results.NotFound("No order for given id found.") : Results.Ok(order);
            })
            .Produces<OrderDto>()
            .WithOpenApi();
    }

    private static void MapPostEndpoints(RouteGroupBuilder orders)
    {
        orders.MapGet("/Create", async ([FromBody] CreateOrderRequestDto request,
                [FromServices] IOrderHandler handler) =>
            {
                if (!request.TryValidate(out var errorMessage))
                {
                    return Results.BadRequest(errorMessage ?? "Invalid request.");
                }

                var isCreated = await handler.Create(request);
                return isCreated
                    ? Results.Ok()
                    : Results.Problem("An order could not be created due to an unexpected error.");
            })
            .Produces<IResult>()
            .WithOpenApi();
    }
}