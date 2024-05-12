using CustomDelegates.Farm.Events;
using CustomDelegates.Infrastructure;

namespace CustomDelegates.Farm.Entities;

public record Dog : IEntity
{
    public Dog()
    {
        EventManager.SubscribeWith<FoodEvent>(this, Subscription);
        EventManager.SubscribeWith<BroadCastEvent>(this, Subscription);
    }

    public void Subscription(IEntity sender, FoodEvent @event)
    {
        Console.WriteLine("Dog food recieved!!");
    }

    public void Subscription(IEntity sender, BroadCastEvent @event)
    {
        throw new NotImplementedException();
    }
}