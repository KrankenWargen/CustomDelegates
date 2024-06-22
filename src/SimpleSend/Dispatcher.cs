using System.Collections.Concurrent;

namespace SimpleSend;

public class Dispatcher : IDispatcher
{
    private delegate void SubscriberMethod(params object[] @params);

    private event SubscriberMethod? Handler;
    private readonly ConcurrentDictionary<ISubscribe, HashSet<SubscriberMethod>> _subscribers = new();

    public void SubscribeWith(Delegate action)
    {
        // guarantee a max of single subscription per entity/subscription combination.
        if (action.Target is not ISubscribe entity)
            return;
        //keep track of subscribed functions for a certain entity, to have any chance of removing/unsubscribing them from the handler later on
        _subscribers.AddOrUpdate(
            entity,
            x => [AnySubscriber],
            (x, list) =>
            {
                list.Add(AnySubscriber);
                return list;
            }
        );
        //subscribe a delegate that upon invocation resolves the delegate based on a
        //condition. (essentially, whether the Delegate (function) has the same parameters as the invoked ones)  
        Handler += AnySubscriber;
        return;

        // local function that represents an anonymous lambda subscriber. a
        // all subscribers must follow the parameter pattern (params x, params IBaseEvent)
        void AnySubscriber(params object[] invokedParams)
        {
            if (action.Method.GetParameters().Length.Equals(invokedParams.Length) &&
                action.Method
                    .GetParameters()
                    .Select(x => x.ParameterType)
                    .Zip(invokedParams, (x, y) => (methodParameterType: x, invokedParameterType: y.GetType()))
                    .All(pair =>
                        pair.methodParameterType == pair.invokedParameterType ||
                        pair.methodParameterType.IsAssignableFrom(pair.invokedParameterType)) &&
                _subscribers.ContainsKey(entity)
               )
            {
                action.DynamicInvoke(invokedParams);
            }
        }
    }

    public void Send(params object[] @params) => Handler?.Invoke(@params);
}