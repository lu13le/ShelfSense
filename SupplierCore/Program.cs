using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupplierCore.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SupplierCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SupplierDatabase")));

