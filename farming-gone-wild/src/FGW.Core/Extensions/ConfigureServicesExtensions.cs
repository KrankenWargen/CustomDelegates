using System;
using System.Collections.Generic;
using System.Linq;
using FGW.Core.Farm;
using FGW.Core.Farm.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace FGW.Core.Extensions;

public static class ConfigureServicesExtensions
{
    internal static IServiceCollection AddSubscribers<IEntity, TEntity>(this IServiceCollection services)
        where IEntity : class where TEntity : class, IEntity => services.AddScoped<IEntity, TEntity>();

    public static IEnumerable<Unit> Subscribe(this IEnumerable<IEntity> entities,
        Func<IEntity, Unit> subscribe) =>
        entities.Select(subscribe);

    public static void GameLaunch(this IEnumerable<Unit> unit, Action<IEnumerable<Unit>> launch) => launch(unit);
}