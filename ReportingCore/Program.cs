﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportingCore.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReportingCoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReportingDatabase")));