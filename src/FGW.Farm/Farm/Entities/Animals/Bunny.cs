using FGW.Farm.Farm.Entities.Interfaces;
using FGW.Farm.Farm.Events;

namespace FGW.Farm.Farm.Entities.Animals;

[Subscribe]
public record Bunny : IFarmEntity
{
   public void FoodSubscription(object sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    public void SleepSubscription(object sender, SleepEvent @event)
    {
        Console.WriteLine("Bunny sleeping");
    }


    public void Subscription(object sender, BroadCastFarmEvent farmEvent)
    {
        throw new NotImplementedException();
    }
}