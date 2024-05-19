using FGW.Core;
using FGW.Infrastructure;

var builder = WebApplication.CreateBuilder();


builder.Services
    .AddCoreServices()
    .AddInfrastructureServices();


var app = builder.Build();
app.Run();