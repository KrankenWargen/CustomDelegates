﻿using FGW.Core.Farm.Entities.Interfaces;
using FGW.Core.Farm.Events;

namespace FGW.Core.Farm.Entities.Animals;

[Subscribe]
public record Cat(string Name, int AgeInMonths) : IFarmEntity
{
    public void FoodSubscription(IEntity sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    public void SleepSubscription(IEntity sender, SleepEvent @event)
    {
        Console.WriteLine($"{Name} sleeping");
    }


    public void Subscription(IEntity sender, BroadCastFarmEvent farmEvent)
    {
        throw new NotImplementedException();
    }

 
}