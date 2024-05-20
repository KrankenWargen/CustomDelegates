using FGW.Core.Farm.Entities.Interfaces;
using FGW.Core.Farm.Events;

namespace FGW.Core.Farm.Entities;

[Subscribe]
public record Farmer : IFarmEntity
{
    public void FoodSubscription(IEntity sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    public void SleepSubscription(IEntity sender, SleepEvent @event)
    {
        Console.WriteLine("Farmer sleeping");
    }

    public void DonationSubscription(IEntity sender, DonationEvent @event)
    {
        Console.WriteLine($"We received: {@event.Money}");
    }

    public void Subscription(IEntity sender, BroadCastFarmEvent farmEvent)
    {
        Console.WriteLine("Farmer sleeping");
    }
}