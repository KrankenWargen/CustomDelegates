using FGW.Core.Farm.Entities.Interfaces;
using FGW.Core.Farm.Events;

namespace FGW.Core.Farm.Entities;

[Subscribe]
public record Farmer : IFarmEntity
{
    public void FoodSubscription(object sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    public void SleepSubscription(object sender, SleepEvent @event)
    {
        Console.WriteLine("Farmer sleeping");
    }

    public void DonationSubscription(object sender, DonationEvent @event)
    {
        Console.WriteLine($"We received: {@event.Money}");
    }

    public void Subscription(object sender, BroadCastFarmEvent farmEvent)
    {
        Console.WriteLine("Farmer sleeping");
    }
}