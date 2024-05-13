using CustomDelegates.Farm.Events;
using CustomDelegates.Infrastructure;

namespace CustomDelegates.Farm.Entities;

public record Dog : IEntity
{
    public Dog()
    {
        this.SubscribeWith<FoodEvent>(FoodSubscription);
        this.SubscribeWith<BroadCastEvent>(Subscription);
    }

    public void FoodSubscription(IEntity sender, FoodEvent @event)
    {
        Console.WriteLine("Dog food recieved!!");
    }

    public void Subscription(IEntity sender, BroadCastEvent @event)
    {
        throw new NotImplementedException();
    }
}