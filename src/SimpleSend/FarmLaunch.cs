using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleSend.Extensions;
using Delegate = System.Delegate;

namespace SimpleSend;

//TODO see if can launch the class without launching a server thru app.run
internal class FarmLaunch(IServiceProvider serviceProvider, IDispatcher dispatcher) : IHostedService, ISubscribe
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        scope.ServiceProvider
            .GetRequiredService<IEnumerable<ISubscribe>>()
            .Register(Methods);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private Unit Methods(ISubscribe entity)
    {
        var type = entity.GetType();
        type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m =>
                m.GetParameters().Length != 0 &&
                m.GetParameters()
                    .Except([m.GetParameters().Reverse().First()])
                    .All(x => !typeof(IBaseEvent).IsAssignableFrom(x.ParameterType) &&
                              !(typeof(IBaseEvent) == x.ParameterType))
            )
            .Where(m => typeof(IBaseEvent).IsAssignableFrom(
                m.GetParameters()
                    .Reverse()
                    .First()
                    .ParameterType)
            )
            .Distinct()
            .ToList()
            .ForEach(method =>
            {
                var actionDelegate = Delegate.CreateDelegate(
                    Helper.GetDelegateType(method), entity,
                    method);
                var callSubscriptionsMethod = typeof(FarmLaunch).GetMethod(
                    nameof(CallSubscriptions),
                    BindingFlags.NonPublic | BindingFlags.Instance);
                callSubscriptionsMethod!
                    .Invoke(this, [actionDelegate]);
            });


        return Unit.Default;
    }


    private void CallSubscriptions(Delegate @this)
    {
        dispatcher.SubscribeWith(@this);
    }
}