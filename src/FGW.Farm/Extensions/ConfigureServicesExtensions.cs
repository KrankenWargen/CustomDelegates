using FGW.Core.Farm;
using FGW.Core.Farm.Entities.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FGW.Core.Extensions;

public static class ConfigureServicesExtensions
{
    internal static IServiceCollection AddSubscribers<TAEntity>(
        this IServiceCollection services,
        TAEntity entity
    ) where TAEntity : class, IEntity =>
        services.AddScoped<IEntity>(_ => entity);


    public static bool Subscribe(this IEnumerable<IEntity> entities,
        Func<IEntity, Unit> subscribeEntity) =>
        entities.Select(subscribeEntity).All(x => x == Unit.Default);

    public static void GameLaunch(this bool subscribedAll, Action launch)
    {
        if (subscribedAll)
        {
            launch();
        }
    }
}