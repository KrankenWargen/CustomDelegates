using CustomDelegates.Farm.Events;

namespace CustomDelegates.Farm.Entities;

public record Cat : IEntity
{
    public void Subscriber(object sender, IBaseEvent @event)
    {
        Console.WriteLine("Cat recieved");
    }
}