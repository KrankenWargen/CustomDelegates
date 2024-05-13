using CustomDelegates.Farm.Events;
using CustomDelegates.Infrastructure;

namespace CustomDelegates.Farm.Entities.Crops;

public record Onion : IEntity
{
    public Onion()
    {
        this.SubscribeWith<FoodEvent>(FoodSubscription);
        this.SubscribeWith<SleepEvent>(SleepSubscription);
        this.SubscribeWith<BroadCastEvent>(Subscription);
    }


    private static void FoodSubscription(IEntity sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    private static void SleepSubscription(IEntity sender, SleepEvent @event)
    {
        Console.WriteLine("Cat sleeping");
    }

    public void Subscription(IEntity sender, BroadCastEvent @event)
    {
        Console.WriteLine("what do you want!!");
    }
}