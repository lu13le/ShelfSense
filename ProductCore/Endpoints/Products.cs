using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProductCore.Handlers.Interfaces;
using ProductCore.Mapping;
using ProductCore.Models.Dtos;

namespace ProductCore.Endpoints;

public static class Products
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder routes, string prefix)
    {
        var products = routes.MapGroup($"{prefix}/Products").WithTags("Products");

        MapGetProductEndpoints(products);
    }

    private static void MapGetProductEndpoints(RouteGroupBuilder products)
    {
        products.MapGet("/GetById", async ([FromQuery] Guid id,
                [FromServices] IProductHandler handler) =>
            {
                var product = await handler.GetById(id);
                return product is null
                    ? Results.NotFound("No product for given id found.")
                    : Results.Ok(product.ToProductDto());
            })
            .Produces<ProductDto>()
            .WithOpenApi();

        products.MapGet("/GetAll", async ([FromServices] IProductHandler handler) =>
            {
                var productsList = await handler.GetAll();
                var enumerable = productsList.ToList();
                return enumerable.Count is not 0
                    ? Results.Ok(enumerable.Select(p => p.ToProductDto()))
                    : Results.NoContent();
            })
            .Produces<IEnumerable<ProductDto>>()
            .WithOpenApi();
    }
}