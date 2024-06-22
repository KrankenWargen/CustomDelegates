using Microsoft.Extensions.DependencyInjection;

namespace FGW.Farm;

public static class ConfigureServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddHostedService<FarmLaunch>();
        services.AddSingleton<IDispatcher, Dispatcher>();

        // .AddSubscribers(new Dog())
        // .AddSubscribers(new Horse())
        // .AddSubscribers(new Farmer())
        // .AddSubscribers(new Buyer { Type = Kind.Self })
        // .AddSubscribers(new Seller());


        return services;
    }
}