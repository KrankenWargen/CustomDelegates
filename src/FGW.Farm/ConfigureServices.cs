using FGW.Farm.Extensions;
using FGW.Farm.Farm.Entities;
using FGW.Farm.Farm.Entities.Animals;
using FGW.Farm.Farm.Entities.Interfaces;
using FGW.Farm.Farm.Entities.Stakeholders;
using Microsoft.Extensions.DependencyInjection;

namespace FGW.Farm;

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