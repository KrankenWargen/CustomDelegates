using System.Collections.Concurrent;

namespace SimpleSend;

internal class SimpleManager : ISender
{
    private readonly ConcurrentDictionary<Type, HashSet<Delegate>> _subscribers = new();

    public void SubscribeWith(Delegate action)
    {
        // guarantee a max of single subscription per entity/subscription combination.
        if (action.Target is not ISubscribe)
            return;
        //keep track of subscribed functions for a certain entity, to have any chance of removing/unsubscribing them from the handler later on
        _subscribers.AddOrUpdate(
            action.Method.GetParameters().Select(x => x.ParameterType).Last(),
            x => [action],
            (x, list) =>
            {
                list.Add(action);
                return list;
            }
        );
    }

    public async Task Send(params object[] @params)
    {
        if (TryMatchHandler(@params.Select(p => p.GetType()).ToArray(), out var matchedHandlers))
        {
            foreach (var matchedHandler in matchedHandlers)
            {
                if (matchedHandler.Method.ReturnType == typeof(Task))
                {
                    await (Task)matchedHandler.DynamicInvoke(@params)!;
                }
                else if (matchedHandler.Method.ReturnType == typeof(void))
                {
                    matchedHandler.DynamicInvoke(@params);
                }
            }
        }
    }

    private bool TryMatchHandler(IReadOnlyCollection<Type> @params, out HashSet<Delegate> matchedHandlers)
    {
        // local function that represents an anonymous lambda subscriber. a
        // all subscribers must follow the parameter pattern (params x, params IBaseEvent)
        matchedHandlers = [];
        var @event = @params.Last();
        if (!_subscribers.TryGetValue(@event, out var handlers)) return false;
        matchedHandlers = handlers.Where(action =>
                action.Method.GetParameters().Length == @params.Count &&
                action.Method
                    .GetParameters()
                    .Zip(@params, (x, y) => (handlerParameterType: x.ParameterType, invokedParameterType: y))
                    .All((x) => x.handlerParameterType.IsAssignableFrom(x.invokedParameterType)))
            .ToHashSet();
        return matchedHandlers.Count > 0;
    }
}