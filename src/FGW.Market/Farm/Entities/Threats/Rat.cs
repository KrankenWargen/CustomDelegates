﻿using FGW.Web.Farm.Entities.Interfaces;
using FGW.Web.Farm.Events;

namespace FGW.Web.Farm.Entities.Threats;


public record Rat : IThreat
{
   public void FoodSubscription(object sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    public void SleepSubscription(object sender, SleepEvent @event)
    {
        Console.WriteLine("Cat sleeping");
    }


}