using System.Linq.Expressions;
using System.Reflection;
using FGW.Farm;
using FGW.Farm.Extensions;
using FGW.Farm.Farm;
using FGW.Farm.Farm.Entities.Animals;
using FGW.Farm.Farm.Entities.Interfaces;
using FGW.Farm.Farm.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Delegate = System.Delegate;

namespace FGW.Infrastructure;

//TODO see if can launch the class without launching a server thru app.run
public class FarmLaunch(IServiceProvider serviceProvider) : IHostedService, ISubscribe
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        scope.ServiceProvider
            .GetRequiredService<IEnumerable<ISubscribe>>()
            .Subscribe(RegisterMethods)
            .GameLaunch(() =>
            {
                FarmManager.GetInstance().Publish(new Dog(), new SleepEvent());
                FarmManager.GetInstance().Publish(new Dog(), new SleepEvent(), new FoodEvent());
            });


        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static Unit RegisterMethods(ISubscribe entity)
    {
        var type = entity.GetType();
        type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m =>
                m.GetParameters()
                    .Except(m.GetParameters()
                        .Reverse()
                        .TakeWhile(x => typeof(IBaseEvent).IsAssignableFrom(x.ParameterType)))
                    .All(x => !typeof(IBaseEvent).IsAssignableFrom(x.ParameterType) &&
                              !(typeof(IBaseEvent) == x.ParameterType))
            )
            .SelectMany(m => m.GetParameters()
                .Reverse()
                .TakeWhile(x => typeof(IBaseEvent).IsAssignableFrom(x.ParameterType))
                .Select(_ => m))
            .Distinct()
            .ToList()
            .ForEach(method =>
            {
                var actionDelegate = Delegate.CreateDelegate(
                    FarmHelper.GetDelegateType(method), entity,
                    method);
                var callSubscriptionsMethod = typeof(FarmLaunch).GetMethod(
                    nameof(CallSubscriptions),
                    BindingFlags.NonPublic | BindingFlags.Static);
                callSubscriptionsMethod!
                    .Invoke(null, [actionDelegate]);
            });


        return Unit.Default;
    }


    private static void CallSubscriptions(Delegate @this)
    {
        FarmManager.GetInstance().SubscribeWith(@this);
    }

    public void LetGo(object @this, RIPEvent @event)
    {
        if (@this is ISubscribe entity)
            FarmManager.GetInstance().UnsubscribeFor(entity);
    }
}