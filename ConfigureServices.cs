using CustomDelegates.Models;

namespace CustomDelegates;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureWebServices(this IServiceCollection services)
    {
        services.AddScoped<Dog>();
        services.AddScoped<Cat>();

        return services;
    }
}