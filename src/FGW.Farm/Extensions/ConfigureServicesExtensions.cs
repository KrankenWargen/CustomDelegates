using FGW.Core.Farm;
using FGW.Core.Farm.Entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FGW.Core.Extensions;

public static class ConfigureServicesExtensions
{
    internal static IServiceCollection AddSubscribers<TSubscribe>(
        this IServiceCollection services,
        TSubscribe implementation
    ) where TSubscribe : class, IEntity =>
        services.AddScoped<ISubscribe>(_ => implementation);

    public static IServiceCollection AddSubscribers<TInterface, TSubscribe>(
        this IServiceCollection services
    ) where TInterface : class, ISubscribe where TSubscribe : class, TInterface =>
        services.AddScoped<TInterface, TSubscribe>();

    public static bool Subscribe(this IEnumerable<ISubscribe> entities,
        Func<ISubscribe, Unit> subscribeEntity) =>
        entities.Select(subscribeEntity).All(x => x == Unit.Default);

    public static void GameLaunch(this bool subscribedAll, Action launch)
    {
        if (subscribedAll)
        {
            launch();
        }
    }
}