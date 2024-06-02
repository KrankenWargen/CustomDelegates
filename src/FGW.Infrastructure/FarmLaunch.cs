using System.Reflection;
using FGW.Core;
using FGW.Core.Extensions;
using FGW.Core.Farm;
using FGW.Core.Farm.Entities.Animals;
using FGW.Core.Farm.Entities.Interfaces;
using FGW.Core.Farm.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FGW.Infrastructure;

//TODO disposal after startAsync finishes. 
//TODO see if can launch the class without launching a server thru app.run
[Subscribe]
public class FarmLaunch(IServiceProvider serviceProvider) : IHostedService, ISubscribe
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        scope.ServiceProvider
            .GetRequiredService<IEnumerable<ISubscribe>>()
            .Subscribe(RegisterMethods)
            .GameLaunch(() => FarmManager.GetInstance().Publish(new Dog(), new SleepEvent()));


        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static Unit RegisterMethods(ISubscribe entity)
    {
        var type = entity.GetType();
        if (typeof(ISubscribe).IsAssignableFrom(type) &&
            type.GetCustomAttributes(typeof(Subscribe), false).Length == 1)
        {
            type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.GetParameters().Length is 2
                            && typeof(object).IsAssignableFrom(m.GetParameters()[0].ParameterType) &&
                            typeof(IBaseEvent).IsAssignableFrom(m.GetParameters()[1].ParameterType))
                .ToList()
                .ForEach(method =>
                {
                    var actionDelegate = Delegate.CreateDelegate(
                        typeof(EventHandler<>).MakeGenericType(
                            method.GetParameters()[1].ParameterType), entity,
                        method);
                    var callSubscriptionsMethod = typeof(FarmLaunch).GetMethod(
                        nameof(CallSubscriptions),
                        BindingFlags.NonPublic | BindingFlags.Static);
                    var genericTypeArgument = actionDelegate.GetType().GenericTypeArguments[0];
                    callSubscriptionsMethod!.MakeGenericMethod(genericTypeArgument)
                        .Invoke(null, [actionDelegate]);
                });
        }

        return Unit.Default;
    }


    private static void CallSubscriptions<TEvent>(EventHandler<TEvent> @this) where TEvent : IBaseEvent
    {
        FarmManager.GetInstance().SubscribeWith(@this);
    }

    //TODO Adjust logic to bulk unsubscribe statt method by method to prevent a deceased entity from receiving messages :(
    public void UNDO(object sender, RIPEvent @event)
    {
        var type = sender.GetType();
        if (typeof(ISubscribe).IsAssignableFrom(type) &&
            type.GetCustomAttributes(typeof(Subscribe), false).Length == 1)
        {
            type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.GetParameters().Length is 2
                            && typeof(object).IsAssignableFrom(m.GetParameters()[0].ParameterType) &&
                            typeof(IBaseEvent).IsAssignableFrom(m.GetParameters()[1].ParameterType))
                .Select(method =>
                {
                    var actionDelegate = Delegate.CreateDelegate(
                        typeof(EventHandler<>).MakeGenericType(
                            method.GetParameters()[1].ParameterType), sender,
                        method);
                    var callSubscriptionsMethod = typeof(FarmLaunch).GetMethod(
                        nameof(CallUnSubscriptions),
                        BindingFlags.NonPublic | BindingFlags.Static);
                    var genericTypeArgument = actionDelegate.GetType().GenericTypeArguments[0];
                    return callSubscriptionsMethod!
                        .MakeGenericMethod(genericTypeArgument)
                        .Invoke(null, [actionDelegate]);
                });
        }
    }

    private static void CallUnSubscriptions<TEvent>(EventHandler<TEvent> @this) where TEvent : IBaseEvent
    {
        FarmManager.GetInstance().UnsubscribeFor(@this);
    }
}