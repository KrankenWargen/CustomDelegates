using FGW.Web.Farm.Entities.Interfaces;
using FGW.Web.Farm.Events;

namespace FGW.Web.Farm.Entities.Animals;


public record Horse : IFarmEntity
{
    
   public void FoodSubscription(object sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    public void SleepSubscription(object sender, SleepEvent @event)
    {
        Console.WriteLine("Horse sleeping");
    }


    public void Subscription(object sender, BroadCastFarmEvent farmEvent)
    {
        throw new NotImplementedException();
    }
}