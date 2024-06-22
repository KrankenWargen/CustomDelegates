using FGW.Farm.Extensions;
using FGW.Web.Farm.Entities.Animals;

namespace FGW.Web;

public static class ConfigureServices
{
    
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSubscribers<Cat>();
        

        return services;
    }
}