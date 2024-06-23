using FGW.Web;
using FGW.Web.Farm.Entities.Animals;
using FGW.Web.Farm.Events;
using SimpleSend;

var builder = WebApplication.CreateBuilder();


builder.Services
    .AddCoreServices()
    .AddWebServices();

var app = builder.Build();

app.MapControllers();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", (IOrchestrate orchestrate) =>
    {
        orchestrate.Send(new Dog(), new SleepEvent());
        return "Events dispatched!";
    });
});
app.Run();