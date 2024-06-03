using FGW.Core.Farm.Entities.Interfaces;
using FGW.Core.Farm.Events;

namespace FGW.Core.Farm.Entities.Animals;

[Subscribe]
public record Dog : IFarmEntity
{
    public void FoodSubscription(object sender, FoodEvent @event)
    {
        Console.WriteLine("Dog food recieved!!");
    }

    public void Subscription(object sender, BroadCastFarmEvent farmEvent)
    {
        throw new NotImplementedException();
    }
}