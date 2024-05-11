using CustomDelegates.Farm.Entities;
using CustomDelegates.Farm.Events;

namespace CustomDelegates.Infrastructure;

public delegate void EventObject(IEntity entity, IBaseEvent @event);

public abstract class EventManager
{
    private static event EventObject? Handler;

    public static void SubscribeWith(IEntity entity) => Handler += entity.Subscriber;

    public static void SubscribeWith(List<IEntity> entities) =>
        entities.ForEach(entity => Handler += entity.Subscriber);

    public static void UnsubscribeFor(IEntity entity) => Handler -= entity.Subscriber;

    public static void UnsubscribeFor(List<IEntity> entities) =>
        entities.ForEach(entity => Handler -= entity.Subscriber);

    public static void Publish(IEntity entity, IBaseEvent @event) => Handler?.Invoke(entity, @event);

    public static void Publish(IEntity entity, List<IBaseEvent> @events) =>
        @events.ForEach(@event => Handler?.Invoke(entity, @event));
}