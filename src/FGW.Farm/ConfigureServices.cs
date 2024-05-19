using FGW.Core.Extensions;
using FGW.Core.Farm.Entities;
using FGW.Core.Farm.Entities.Animals;
using Microsoft.Extensions.DependencyInjection;

namespace FGW.Core;

public static class ConfigureServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services
            .AddSubscribers<IEntity, Cat>()
            .AddSubscribers<IEntity, Dog>()
            .AddSubscribers<IEntity, Horse>()
            .AddSubscribers<IEntity, Farmer>();

        return services;
    }
}