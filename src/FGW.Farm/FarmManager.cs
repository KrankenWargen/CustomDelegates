using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;
using FGW.Farm.Farm.Entities.Animals;
using FGW.Farm.Farm.Entities.Interfaces;
using FGW.Farm.Farm.Events;

namespace FGW.Farm;

public class FarmManager
{
    private delegate void SubscriberMethod(params object[] @params);

    private event SubscriberMethod? Handler;
    private static readonly Lazy<FarmManager> Event = new();

    private readonly ConcurrentDictionary<ISubscribe, HashSet<MethodInfo>> _subscribers = new();

    public static FarmManager GetInstance() => Event.Value;

    public void SubscribeWith(Delegate action)
    {
        // guarantee a max of single subscription per entity/subscription combination.
        if (action.Target is not ISubscribe entity || (
                _subscribers.TryGetValue(entity, out var eventRecievers) &&
                eventRecievers.Any(x => x == action.Method)
            )
           ) return;
        //keep track of subscribed functions for a certain entity, to have any chance of removing/unsubscribing them from the handler later on
        _subscribers.AddOrUpdate(
            entity,
            x => [action.Method],
            (x, list) =>
            {
                list.Add(action.Method);
                return list;
            }
        );
        //subscribe a delegate that upon invocation resolves the delegate based on a
        //condition. (essentially, whether the Delegate (function) has the same parameters as the invoked ones)  
        Handler += AnySubscriber;
        return;

        // local function that represents any subscriber. a
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
                        pair.methodParameterType.IsAssignableFrom(pair.invokedParameterType)
                    )
               )
            {
                if (_subscribers.ContainsKey(entity))
                    action.DynamicInvoke(invokedParams);
                else
                    Handler -= AnySubscriber;
            }
        }
    }

    public void UnsubscribeFor(ISubscribe? entity)
    {
        if (entity is null || !_subscribers.TryGetValue(entity, out var registered)) return;
        _subscribers.Remove(entity, out var _);
        
        // TODO understnad how to create a dynamic method 
        foreach (var method in registered)
        {
            var x = DelegateCreator.CreateAdapterDelegate<SubscriberMethod>(method, entity);
            Handler -= x;
        }

        Publish(new Dog(), new SleepEvent());
        Publish(new Dog(), new SleepEvent());
    }

    public void Publish(params object[] @params) => Handler?.Invoke(@params);

    public static class DelegateCreator
    {
        public static TDelegate CreateAdapterDelegate<TDelegate>(MethodInfo methodInfo, object target)
            where TDelegate : Delegate
        {
            var parameters = methodInfo.GetParameters();
            if (parameters.Length != 2 || !(typeof(object) == parameters[0].ParameterType)
                                       || !(typeof(FoodEvent) == parameters[1].ParameterType))
            {
                throw new InvalidOperationException("Method parameters do not match expected types.");
            }

            // Define a dynamic method to host the adapted invocation
            DynamicMethod dynamicMethod = new DynamicMethod(
                "Adapter_" + methodInfo.Name,
                typeof(void),
                new Type[] { typeof(object[]) },
                methodInfo.DeclaringType!);

            ILGenerator il = dynamicMethod.GetILGenerator();

            // Load the target object (if instance method)
            if (!methodInfo.IsStatic)
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_0); // Load the object array (arguments)
                il.Emit(OpCodes.Ldc_I4_0); // Load the index of the first argument
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Castclass, parameters[0].ParameterType); // Cast to IEntity

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldc_I4_1); // Load the index of the second argument
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Castclass, parameters[1].ParameterType); // Cast to SleepEvent

                // Call the method
                il.Emit(OpCodes.Callvirt, methodInfo);
            }

            // Return from the method
            il.Emit(OpCodes.Ret);
            // Create the delegate from the dynamic method

            var xa = dynamicMethod.CreateDelegate(typeof(SubscriberMethod), target);
            return (xa as TDelegate)!;
        }
    }
}