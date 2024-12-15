using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OrderCore.Data.Contexts;
using OrderCore.Data.Repositories;
using OrderCore.Data.Repositories.Interfaces;
using OrderCore.Endpoints;
using OrderCore.Handlers;
using OrderCore.Handlers.Interfaces;

const string prefix = "order-core";
const string version = "v1";
const string versionPrefix = $"/{prefix}/api/{version}";

var builder = WebApplication.CreateBuilder(args);

builder.Environment.EnvironmentName = Environments.Development;

builder.Services.AddDbContext<OrderCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDatabase")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderHandler, OrderHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OrderCore API",
        Version = "v1",
        Description = "API for managing orders in the Inventory Management System."
    });
});

// Add health check services
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("OrderDatabase") ?? string.Empty, name: "Order Database");

var app = builder.Build();

app.MapOrderEndpoints(versionPrefix);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderCore API v1");
        c.RoutePrefix = string.Empty;  // Makes Swagger the root URL
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Map health check endpoint
app.MapHealthChecks("/health");

app.Run();
