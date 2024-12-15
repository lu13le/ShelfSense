﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProductCore.Data.Contexts;
using ProductCore.Data.Repositories;
using ProductCore.Data.Repositories.Interfaces;
using ProductCore.Endpoints;
using ProductCore.Handlers;
using ProductCore.Handlers.Interfaces;

const string prefix = "product-core";
const string version = "v1";
const string versionPrefix = $"/{prefix}/api/{version}";

var builder = WebApplication.CreateBuilder(args);

builder.Environment.EnvironmentName = Environments.Development;

builder.Services.AddDbContext<ProductCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDatabase")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductHandler, ProductHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProductCore API",
        Version = "v1",
        Description = "API for managing orders in the Inventory Management System."
    });
});

// Add health check services
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("ProductDatabase") ?? string.Empty, name: "Product Database");

var app = builder.Build();

app.MapProductEndpoints(versionPrefix);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductCore API v1");
        c.RoutePrefix = string.Empty; //Makes Swagger the root URL
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Map health check endpoint
app.MapHealthChecks("/health");

app.Run();