using System.Reflection;
using FGW.Core;
using FGW.Core.Extensions;
using FGW.Core.Farm;
using FGW.Core.Farm.Entities;
using FGW.Core.Farm.Entities.Animals;
using FGW.Core.Farm.Events;
using Microsoft.Extensions.Hosting;

namespace FGW.Infrastructure;


//TODO disposal after startAsync finishes. 
//TODO see if can launch the class without launching a server thru app.run
public class BindAndDispatch(IEnumerable<IEntity> entities) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        entities
            .Subscribe(x => RegisterMethods(x.GetType()))
            .GameLaunch(_ => EventManager.Publish(new Dog(), new SleepEvent()));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static Unit RegisterMethods(Type type)
    {
        if (typeof(IEntity).IsAssignableFrom(type) &&
            type.GetCustomAttributes(typeof(Subscribe), false).Length == 1)
        {
            type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.GetParameters().Length is 2
                            && typeof(IEntity).IsAssignableFrom(m.GetParameters()[0].ParameterType) &&
                            typeof(IBaseEvent).IsAssignableFrom(m.GetParameters()[1].ParameterType))
                .ToList()
                .ForEach(method =>
                {
                    var actionDelegate = Delegate.CreateDelegate(
                        typeof(Action<,>).MakeGenericType(
                            typeof(IEntity),
                            method.GetParameters()[1].ParameterType), Activator.CreateInstance(type),
                        method);
                    var callSubscriptionsMethod = typeof(BindAndDispatch).GetMethod(
                        nameof(CallSubscriptions),
                        BindingFlags.NonPublic | BindingFlags.Static);
                    var genericTypeArgument = actionDelegate.GetType().GenericTypeArguments[1];
                    callSubscriptionsMethod!.MakeGenericMethod(genericTypeArgument)
                        .Invoke(null, [actionDelegate]);
                });
        }

        return Unit.Default;
    }


    private static void CallSubscriptions<TEvent>(Action<IEntity, TEvent> @this) where TEvent : IBaseEvent
    {
        EventManager.SubscribeWith(@this);
    }
}