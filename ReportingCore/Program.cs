using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ReportingCore.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Environment.EnvironmentName = Environments.Development;

builder.Services.AddDbContext<ReportingCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReportingDatabase")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ReportingCore API",
        Version = "v1",
        Description = "API for managing orders in the Inventory Management System."
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReportingCore API v1"); });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();