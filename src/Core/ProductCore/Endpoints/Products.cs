using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProductCore.Extensions;
using ProductCore.Handlers.Interfaces;
using ProductCore.Models.Dtos;

namespace ProductCore.Endpoints;

public static class Products
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder routes, string prefix)
    {
        var products = routes.MapGroup($"{prefix}/Products").WithTags("Products");
        MapGetEndpoints(products);
        MapPostEndpoints(products);
        MapPatchEndpoints(products);
        MapDeleteEndpoints(products);
    }

    private static void MapGetEndpoints(RouteGroupBuilder products)
    {
        products.MapGet("/GetById", async ([FromQuery] Guid id,
                [FromServices] IProductHandler handler) =>
            {
                var product = await handler.GetById(id);
                return product is null
                    ? Results.NotFound("No product for given id found.")
                    : Results.Ok(product);
            })
            .Produces<ProductDto>()
            .WithOpenApi();

        products.MapGet("/GetAll", async ([FromServices] IProductHandler handler) =>
            {
                var productsList = await handler.GetAll();
                return Results.Ok(productsList);
            })
            .Produces<IEnumerable<ProductDto>>()
            .WithOpenApi();
    }

    private static void MapPostEndpoints(RouteGroupBuilder products)
    {
        products.MapPost("Create",
                async ([FromBody] CreateProductRequestDto request, [FromServices] IProductHandler handler) =>
                {
                    if (!request.TryValidate(out var errorMessage))
                    {
                        return Results.BadRequest(errorMessage ?? "Invalid request.");
                    }
                    
                    var isCreated = await handler.Create(request);
                    return isCreated
                        ? Results.Ok()
                        : Results.Problem("The product could not be created due to an unexpected error.");
                })
            .Produces<IResult>()
            .WithOpenApi();
    }

    private static void MapPatchEndpoints(RouteGroupBuilder products)
    {
        products.MapPatch("UpdatePrice",
                async ([FromBody] UpdateProductPriceRequestDto request, [FromServices] IProductHandler handler) =>
                {
                    if (!request.TryValidate(out var errorMessage))
                    {
                        return Results.BadRequest(errorMessage ?? "Invalid request.");
                    }
                    
                    var isUpdated = await handler.UpdatePrice(request);
                    return isUpdated
                        ? Results.Ok($"The product with id: {request.Id} is successfully updated.")
                        : Results.Problem("The product cannot be updated due to an unexpected error.");
                })
            .Produces<IResult>()
            .WithOpenApi();

        products.MapPatch("UpdateQuantity",
                async ([FromBody] UpdateProductQuantityRequestDto request, [FromServices] IProductHandler handler) =>
                {
                    if (!request.TryValidate(out var errorMessage))
                    {
                        return Results.BadRequest(errorMessage ?? "Invalid request.");
                    }
                    
                    var isUpdated = await handler.UpdateQuantity(request);
                    return isUpdated
                        ? Results.Ok($"The product with id: {request.Id} is successfully updated.")
                        : Results.Problem("The product cannot be updated due to an unexpected error.");
                })
            .Produces<IResult>()
            .WithOpenApi();
    }

    private static void MapDeleteEndpoints(RouteGroupBuilder products)
    {
        products.MapDelete("Delete",
            async ([FromQuery] Guid id, [FromServices] IProductHandler handler) =>
            {
                var isDeleted = await handler.Delete(id);
                return isDeleted
                    ? Results.NoContent()
                    : Results.Problem("The product cannot be deleted due to an unexpected error.");
            });
    }
}