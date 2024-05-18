using System.Reflection;
using CustomDelegates.Farm.Entities;
using CustomDelegates.Farm.Events;

namespace CustomDelegates.Infrastructure;

public class Subscribe : Attribute
{
    private static void CallSubscriptions<TEvent>(Action<IEntity, TEvent> @this) where TEvent : IBaseEvent
    {
        EventManager.SubscribeWith(@this);
    }

    public static void RegisterSubscriptions()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(IEntity).IsAssignableFrom(type) &&
                    type.GetCustomAttributes(typeof(Subscribe), false).Length == 1)
                {
                    type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .Where(m => m.GetParameters().Length == 2
                                    && m.GetParameters()[0].ParameterType == typeof(IEntity) &&
                                    m.GetParameters()[1].ParameterType.GetInterface("IBaseEvent") == typeof(IBaseEvent))
                        .Select(x =>
                        {
                            var actionDelegate = Delegate.CreateDelegate(
                                typeof(Action<,>).MakeGenericType(
                                    typeof(IEntity),
                                    x.GetParameters()[1].ParameterType), Activator.CreateInstance(type),
                                x);
                            var callSubscriptionsMethod = typeof(Subscribe).GetMethod(
                                "CallSubscriptions",
                                BindingFlags.NonPublic | BindingFlags.Static);
                            var genericTypeArgument = actionDelegate.GetType().GenericTypeArguments[1];
                            var genericCallSubscriptionsMethod =
                                callSubscriptionsMethod!.MakeGenericMethod(genericTypeArgument);
                            return genericCallSubscriptionsMethod.Invoke(null, [actionDelegate]);
                        }).ToArray();
                }
            }
        }
    }
}