using FGW.Farm.Farm.Entities.Interfaces;
using FGW.Farm.Farm.Events;

namespace FGW.Farm.Farm.Entities.Stakeholders;

public enum Kind
{
    Self,
    Commerical,
    Contractual,
    MediumBuissness,
    LargeBuisness,
    InternaltionalBuisness,
    Massive
}

[Subscribe]
public record Buyer : IEntity
{
    public required Kind Type { get; init; }

    public void FoodSubscription(object sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    public void SleepSubscription(object sender, SleepEvent @event)
    {
        Console.WriteLine($"{Type} sleeping");
    }
}