using FGW.Farm.Extensions;
using FGW.Farm.Farm.Entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FGW.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHostedService<FarmLaunch>();
        services.AddScoped<ISubscribe, FarmLaunch>();
        return services;
    }
}