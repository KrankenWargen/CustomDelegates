using FGW.Core.Farm.Entities.Interfaces;
using FGW.Core.Farm.Events;

namespace FGW.Core.Farm.Entities.Animals;

[Subscribe]
public record Cat(string Name, int AgeInMonths) : IFarmEntity
{
    public void FoodSubscription(object sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    public void SleepSubscription(object sender, SleepEvent @event)
    {
        Console.WriteLine($"{Name} sleeping");
        if (Name == "Nono") FarmManager.GetInstance().Publish(this, new RIPEvent());
    }


    public void Subscription(object sender, BroadCastFarmEvent farmEvent)
    {
        throw new NotImplementedException();
    }
}