using CustomDelegates.Farm.Events;

namespace CustomDelegates.Farm.Entities;

public record Dog : IEntity
{
    public void Subscriber(object sender, IBaseEvent @event)
    {
        Console.WriteLine("Dog recieved");
    }
}