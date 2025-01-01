using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductCore.Endpoints;
using ProductCore.Extensions;
using Serilog;

const string prefix = "product-core";
const string version = "v1";
const string versionPrefix = $"/{prefix}/api/{version}";

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.ConfigureSerilog();

//Logging filters
builder.AddLoggingFilters();

// Add Serilog as the logging provider
builder.Host.UseSerilog();

builder.Environment.EnvironmentName = Environments.Development;

// Add DbContext
builder.Services.AddDbContext(builder.Configuration);

// Add repositories and handlers
builder.Services.AddServices();

// Add Swagger documentation
builder.Services.AddSwagger();

// Add Health Checks
builder.Services.AddHealthChecks(builder.Configuration);

// Add Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure Swagger and API Key Validation
app.ConfigureSwaggerAndApiKeyValidation();

// Map endpoints
app.MapProductEndpoints(versionPrefix);

app.UseHttpsRedirection();

// Enable static files (for Swagger UI assets)
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Map health check endpoint
app.MapHealthChecks("/health");

app.Run();