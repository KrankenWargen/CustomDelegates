using FGW.Core;
using FGW.Infrastructure;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder();


builder.Services
    .AddCoreServices()
    .AddInfrastructureServices();


var app = builder.Build();
app.Run();