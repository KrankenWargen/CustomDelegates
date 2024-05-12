using CustomDelegates.Farm.Entities;

namespace CustomDelegates.Infrastructure;

public static class EventExtensions
{
    public static void Register<TEvent>(this IEntity entity, Action<IEntity, TEvent> scheme) =>
        EventManager.SubscribeWith(entity, scheme);

    public static void Leave<TEvent>(this IEntity entity) => EventManager.UnsubscribeFor<TEvent>(entity);
}