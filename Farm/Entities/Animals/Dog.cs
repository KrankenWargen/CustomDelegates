using CustomDelegates.Farm.Events;
using CustomDelegates.Infrastructure;

namespace CustomDelegates.Farm.Entities.Animals;

[Subscribe]
public record Dog : IEntity
{
    public void FoodSubscription(IEntity sender, FoodEvent @event)
    {
        Console.WriteLine("Dog food recieved!!");
    }

    public void Subscription(IEntity sender, BroadCastEvent @event)
    {
        throw new NotImplementedException();
    }
}