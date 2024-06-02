using System.Collections.Concurrent;
using FGW.Core.Farm.Entities.Animals;
using FGW.Core.Farm.Entities.Interfaces;
using FGW.Core.Farm.Events;

namespace FGW.Core;

internal class DelegateEqualityComparer : IEqualityComparer<Delegate>
{
    public bool Equals(Delegate? x, Delegate? y)
    {
        return ReferenceEquals(x, y) || (
            x != null &&
            y != null &&
            x.Target != null &&
            x.Target.Equals(y.Target) &&
            x.Method == y.Method
        );
    }

    public int GetHashCode(Delegate obj)
    {
        return HashCode.Combine(obj.Method, obj.Target);
    }
}

public class FarmManager
{
    private event EventHandler<IBaseEvent>? Handler;
    private static readonly Lazy<FarmManager> Event = new();

    private readonly ConcurrentDictionary<Delegate, EventHandler<IBaseEvent>> _subscribers =
        new(new DelegateEqualityComparer());

    public static FarmManager GetInstance() => Event.Value;

    public void SubscribeWith<TEvent>(EventHandler<TEvent> action) where TEvent : IBaseEvent
    {
        // guarantee a max of single subscription per entity/subscription combination.
        if (_subscribers.TryGetValue(action, out var _)) return;

        _subscribers[action] = (@this, @event) =>
        {
            if (@event is TEvent thisEvent) action(@this, thisEvent);
        };
        Handler += _subscribers[action];
    }

    public void UnsubscribeFor<TEvent>(EventHandler<TEvent> action) where TEvent : IBaseEvent
    {
        if (!_subscribers.TryGetValue(action, out var registered)) return;
        Handler -= registered;
        _subscribers.Remove(action, out var _);
       Publish(new Dog(), new SleepEvent());
    }

    public void Publish(object? entity, IBaseEvent @event) => Handler?.Invoke(entity, @event);
}