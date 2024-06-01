using FGW.Core.Farm.Entities.Interfaces;

namespace FGW.Infrastructure;

public interface IEventManager
{
    public void SubscribeWith<TEvent>(Action<IEntity, TEvent> action) where TEvent : IBaseEvent;
    public void Publish(IEntity entity, IBaseEvent @event);
    public void UnsubscribeFor(EventManager.EventSchema action);
}

internal class DelegateEqualityComparer : IEqualityComparer<Delegate>
{
    public bool Equals(Delegate x, Delegate y)
    {
        return ReferenceEquals(x, y) || (x.Target.Equals(y.Target) && x.Method == y.Method);
    }

    public int GetHashCode(Delegate obj)
    {
        return HashCode.Combine(obj.Method, obj.Target);
    }
}

public class EventManager : IEventManager
{
    public delegate void EventSchema(IEntity entity, IBaseEvent @event);

    private static event EventSchema? Handler;
    private readonly HashSet<Delegate> Subscribers = new(new DelegateEqualityComparer());

    public void SubscribeWith<TEvent>(Action<IEntity, TEvent> action) where TEvent : IBaseEvent
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

    public void UnsubscribeFor(EventSchema action)
    {
        if (Subscribers.Remove(action))
        {
            Handler -= action;
        }
    }


    public void Publish(IEntity entity, IBaseEvent @event) => Handler?.Invoke(entity, @event);

    public static void Publish(IEntity entity, List<IBaseEvent> @events) =>
        @events.ForEach(@event => Handler?.Invoke(entity, @event));
}