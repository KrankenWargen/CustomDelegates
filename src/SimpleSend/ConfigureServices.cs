using Microsoft.Extensions.DependencyInjection;

namespace SimpleSend;

public static class ConfigureServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddHostedService<RegistrationService>();
        services.AddSingleton<IOrchestrate, Orchestrator>();

        // .AddSubscribers(new Dog())
        // .AddSubscribers(new Horse())
        // .AddSubscribers(new Farmer())
        // .AddSubscribers(new Buyer { Type = Kind.Self })
        // .AddSubscribers(new Seller());


        return services;
    }
}