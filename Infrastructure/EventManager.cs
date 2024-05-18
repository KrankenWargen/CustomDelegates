using System.Runtime.CompilerServices;
using CustomDelegates.Farm.Entities;
using CustomDelegates.Farm.Events;

namespace CustomDelegates.Infrastructure;

public static class EventManager
{
    public delegate void EventSchema(IEntity entity, IBaseEvent @event);

    private static event EventSchema? Handler;
    private static readonly HashSet<Delegate> Subscribers = [];

    public static void SubscribeWith<TEvent>(Action<IEntity, TEvent> action) where TEvent : IBaseEvent
    {
        // guarantee a max of single subscription per entity/subscription combination.
        if (Subscribers.Add(action))
        {
            Handler += (@this, @event) =>
            {
                if (@event is TEvent @thisEvent) action(@this, @thisEvent);
            };
        }
    }

    internal static void UnsubscribeFor(EventSchema action)
    {
        if (Subscribers.Remove(action))
        {
            Handler -= action;
        }
    }


    public static void Publish(IEntity entity, IBaseEvent @event) => Handler?.Invoke(entity, @event);

    public static void Publish(IEntity entity, List<IBaseEvent> @events) =>
        @events.ForEach(@event => Handler?.Invoke(entity, @event));
}