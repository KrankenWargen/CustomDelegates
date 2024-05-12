using CustomDelegates.Farm.Events;
using CustomDelegates.Infrastructure;

namespace CustomDelegates.Farm.Entities;

public record Cat : IEntity
{
    public Cat()
    {
        this.Register<FoodEvent>(Subscription);
        this.Register<BroadCastEvent>(Subscription);
        this.Register<SleepEvent>(Subscription);
    }


    private static void Subscription(IEntity sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    private static void Subscription(IEntity sender, SleepEvent @event)
    {
        Console.WriteLine("Cat sleeping");
    }

    public void Subscription(IEntity sender, BroadCastEvent @event)
    {
        Console.WriteLine("what do you want!!");
    }
}