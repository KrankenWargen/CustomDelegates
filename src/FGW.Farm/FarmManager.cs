using System.Collections.Concurrent;
using FGW.Farm.Farm.Entities.Animals;
using FGW.Farm.Farm.Entities.Interfaces;
using FGW.Farm.Farm.Events;

namespace FGW.Farm;

public class FarmManager
{
    private event EventHandler<IBaseEvent>? Handler;
    private static readonly Lazy<FarmManager> Event = new();

    private readonly ConcurrentDictionary<ISubscribe, List<EventHandler<IBaseEvent>>> _subscribers = new();

    public static FarmManager GetInstance() => Event.Value;

    public void SubscribeWith<TEvent>(EventHandler<TEvent> action) where TEvent : IBaseEvent
    {
        // guarantee a max of single subscription per entity/subscription combination.
        if (action.Target is not ISubscribe entity || (
                _subscribers.TryGetValue(entity, out var eventRecievers) &&
                eventRecievers.Any(x => x.Method == action.Method)
            )
           ) return;
        //keep track of subscribed functions for a certain entity, to have any chance of removing/unsubscribing them from the handler later on
        _subscribers.AddOrUpdate(
            entity,
            x => [OnHandler],
            (x, list) =>
            {
                list.Add(OnHandler);
                return list;
            }
        );
        //subscribe a delegate that upon invocation resolves the Action delegate based on a
        //condition. (whether the Action [TEvent] is the same as the invoked one)  
        Handler += OnHandler;
        return;

        // local function/delegate
        void OnHandler(object? @this, IBaseEvent @event)
        {
            if (_subscribers.ContainsKey(entity) && @event is TEvent thisEvent) action(@this, thisEvent);
        }
    }

    public void UnsubscribeFor(ISubscribe? entity)
    {
        if (entity is null || !_subscribers.TryGetValue(entity, out var registered)) return;
        _subscribers.Remove(entity, out var _);
        registered.ForEach(x => Handler -= x);
        Publish(new Dog(), new FoodEvent());
    }

    public void Publish(object? entity, IBaseEvent @event) => Handler?.Invoke(entity, @event);
}