﻿using FGW.Web.Farm.Entities.Interfaces;
using FGW.Web.Farm.Events;

namespace FGW.Web.Farm.Entities.Stakeholders;

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