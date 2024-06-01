using FGW.Core.Farm.Entities.Interfaces;
using FGW.Core.Farm.Events;

namespace FGW.Core.Farm.Entities.Stakeholders;

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

    public void FoodSubscription(IEntity sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    public void SleepSubscription(IEntity sender, SleepEvent @event)
    {
        Console.WriteLine($"{Type} sleeping");
    }
}