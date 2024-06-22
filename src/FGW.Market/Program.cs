using FGW.Farm;
using FGW.Farm.Extensions;
using FGW.Web;
using FGW.Web.Farm.Entities.Animals;
using FGW.Web.Farm.Events;

var builder = WebApplication.CreateBuilder();


builder.Services
    .AddCoreServices()
    .AddWebServices();


var app = builder.Build();

app.MapControllers();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        using (var scope = builder.Services.BuildServiceProvider().CreateScope())
        {
            var dispatcher = scope.ServiceProvider.GetRequiredService<IDispatcher>();
            dispatcher.Send(new Dog(), new SleepEvent());
        }
        await context.Response.WriteAsync("Events dispatched!");
    });
});
app.Run();





