using FGW.Farm.Farm.Entities.Interfaces;
using FGW.Farm.Farm.Events;

namespace FGW.Farm.Farm.Entities.Threats;


public record Wolf : IThreat
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