using FGW.Core.Extensions;
using FGW.Core.Farm.Entities;
using FGW.Core.Farm.Entities.Animals;
using FGW.Core.Farm.Entities.Interfaces;
using FGW.Core.Farm.Entities.Stakeholders;
using Microsoft.Extensions.DependencyInjection;

namespace FGW.Core;

public static class ConfigureServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services
            .AddSubscribers(new Cat("Nono", 3))
            .AddSubscribers(new Cat("Nono", 3))
            .AddSubscribers(new Cat("Kitten", 16));
        // .AddSubscribers(new Dog())
        // .AddSubscribers(new Horse())
        // .AddSubscribers(new Farmer())
        // .AddSubscribers(new Buyer { Type = Kind.Self })
        // .AddSubscribers(new Seller());


        return services;
    }
}