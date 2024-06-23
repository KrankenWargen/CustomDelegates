### SimpleSend

A lightweight general purpose library for implementing the mediator pattern through the language
delegates. 


#### Quickstart

```csharp
// register SimpleSend
builder.Services.AddSimpleSend();

// implement the interface ISubscribe for classes which contain event handlers
public record Cat : ISubscribe
{
  // make sure that the event must be the last parameter in the method (sync or async), otherwise the method is ignored for the current version 1.0.0.  
  // other parameters can be of any number/type  
  public void SleepSubscription(object sender, SleepEvent @event)
    {
        Console.WriteLine($"{Name} sleeping");
    }

}

// register the subscriber classes
services.AddSubscribers<Cat>();

// implement the interface IBaseEvent for event types
public struct SleepEvent : IBaseEvent;

// use the send method for sync or async publishing
ISender sender => sender.Send(new Dog(), new SleepEvent());   
```

