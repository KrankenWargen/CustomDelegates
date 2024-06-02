using FGW.Core.Farm.Entities.Interfaces;
using FGW.Core.Farm.Events;

namespace FGW.Core.Farm.Entities.Animals;

[Subscribe]
public record Cow : IFarmEntity
{

   public void FoodSubscription(object sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    public void SleepSubscription(object sender, SleepEvent @event)
    {
        Console.WriteLine("Cat sleeping");
    }


    public void Subscription(object sender, BroadCastFarmEvent farmEvent)
    {
        throw new NotImplementedException();
    }
}