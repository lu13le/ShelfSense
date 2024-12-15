using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProductCore.Handlers.Interfaces;
using ProductCore.Mapping;

namespace ProductCore.Endpoints;

public static class Products
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder routes, string prefix)
    {
        var products = routes.MapGroup($"{prefix}/Products").WithTags("Products");

        products.MapGet("/GetById", async ([FromQuery] Guid id,
            [FromServices] IProductHandler handler) =>
        {
            var product = await handler.GetById(id);
            return product is null
                ? Results.NotFound("No product for given id found.")
                : Results.Ok(product.ToProductDto());
        });
    }
}