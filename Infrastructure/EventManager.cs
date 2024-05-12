using CustomDelegates.Farm.Entities;
using CustomDelegates.Farm.Events;

namespace CustomDelegates.Infrastructure;

public delegate void EventSchema(IEntity entity, IBaseEvent @event);

public abstract class EventManager
{
    public static event EventSchema? Handler;
    private static readonly Dictionary<IEntity, List<(EventSchema schema, Type eventType)>> Subscribers = new();

    public static void SubscribeWith<TEvent>(IEntity entity, Action<IEntity, TEvent> action)
    {
        // Capture the context in which the function is called in through the EventSchema delegate, and execute
        // the delegate (.Invoke), once the multicast delegate matches its signature with the calling parameters.
        // The execution happens within the publish method
        EventSchema eventSchema = (sender, @event) =>
        {
            if (@event is TEvent typedEvent)
            {
                action(sender, typedEvent);
            }
        };

        // guarantee a max of single subscription per entity/subscription combination.
        if (Subscribers.TryGetValue(entity, out var handlers))
        {
            if (handlers.Any(entry => entry.eventType == typeof(TEvent))) return;
            handlers.Add((eventSchema, typeof(TEvent)));
            Handler += eventSchema;
        }
        else
        {
            Subscribers.Add(entity, new List<(EventSchema, Type)> { (eventSchema, typeof(TEvent)) });
            Handler += eventSchema;
        }
    }

    public static void UnsubscribeFor<TEvent>(IEntity entity)
    {
        if (Subscribers.TryGetValue(entity, out var handlers))
        {
            handlers.ForEach(entry =>
            {
                if (entry.eventType == typeof(TEvent))
                {
                    handlers.Remove(entry);
                }
            });
        }
    }


    public static void Publish(IEntity entity, IBaseEvent @event) => Handler?.Invoke(entity, @event);

    public static void Publish(IEntity entity, List<IBaseEvent> @events) =>
        @events.ForEach(@event => Handler?.Invoke(entity, @event));
}