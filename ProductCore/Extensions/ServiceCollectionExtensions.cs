using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProductCore.Data.Contexts;
using ProductCore.Data.Repositories;
using ProductCore.Data.Repositories.Interfaces;
using ProductCore.Handlers;
using ProductCore.Handlers.Interfaces;

namespace ProductCore.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        // Add repositories and handlers
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductHandler, ProductHandler>();
    }

    public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        // Add health checks
        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("ProductDatabase") ?? string.Empty,
                name: "Product Database");
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ProductCore API",
                Version = "v1",
                Description = "API for managing orders in the Inventory Management System."
            });

            // Add API Key security definition
            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Name = "X-Api-Key",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Description = "Enter your API key here"
            });

            // Apply security globally to all endpoints
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void ConfigureSwaggerAndApiKeyValidation(this WebApplication app)
    {
        var apiKey = app.Configuration["ApiKeySettings:SecretKey"];

        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductCore API v1");
            options.RoutePrefix = "";
        });

        app.Use(async (context, next) =>
        {
            var path = context.Request.Path.Value;

            // Skip API key validation for Swagger UI and assets
            if (path != null && (
                    path.StartsWith("/swagger") || // Swagger JSON
                    path.Contains("swagger-ui") || // Swagger UI assets
                    path.StartsWith("/index.html") || // Swagger UI page
                    path.StartsWith("/swagger/v1") // Swagger v1 docs
                ))
            {
                await next();
                return;
            }

            // Validate API Key for other requests
            if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey) || extractedApiKey != apiKey)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid or missing API Key.");
                return;
            }

            await next();
        });
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<ProductCoreContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ProductDatabase")));
    }
}