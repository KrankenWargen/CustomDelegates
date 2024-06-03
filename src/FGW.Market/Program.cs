using FGW.Farm;
using FGW.Infrastructure;
using FGW.Web;

var builder = WebApplication.CreateBuilder();


builder.Services
    .AddCoreServices()
    .AddInfrastructureServices()
    .AddWebServices();


var app = builder.Build();

app.MapControllers();
app.Run();