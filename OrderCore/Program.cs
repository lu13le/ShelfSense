using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderCore.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDatabase")));