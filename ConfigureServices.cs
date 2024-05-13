using CustomDelegates.Farm.Entities;
using CustomDelegates.Farm.Entities.Animals;

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