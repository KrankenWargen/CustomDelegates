namespace FGW.Web;

public static class ConfigureServices
{
    
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }
}