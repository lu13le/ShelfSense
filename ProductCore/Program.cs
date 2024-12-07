using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCore.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDatabase")));