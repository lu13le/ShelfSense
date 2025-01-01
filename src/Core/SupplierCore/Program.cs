using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SupplierCore.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Environment.EnvironmentName = Environments.Development;

builder.Services.AddDbContext<SupplierCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SupplierDatabase")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SupplierCore API",
        Version = "v1",
        Description = "API for managing orders in the Inventory Management System."
    });
});

// Add health check services
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("SupplierDatabase") ?? string.Empty, name: "Supplier Database");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "SupplierCore API v1"); });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Map health check endpoint
app.MapHealthChecks("/health");

app.Run();

