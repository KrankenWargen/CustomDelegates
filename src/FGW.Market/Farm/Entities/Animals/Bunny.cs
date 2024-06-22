using FGW.Web.Farm.Entities.Interfaces;
using FGW.Web.Farm.Events;

namespace FGW.Web.Farm.Entities.Animals;

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

    public void Subscription(BroadCastFarmEvent farmEvent, params object[] x)
    {
        throw new NotImplementedException();
    }

    public void Subscription(object sender, BroadCastFarmEvent farmEvent)
    {
        throw new NotImplementedException();
    }
}