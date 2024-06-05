using FGW.Farm.Farm.Entities.Interfaces;
using FGW.Farm.Farm.Events;

namespace FGW.Farm.Farm.Entities.Animals;


public record Cat(string Name, int AgeInMonths) : IFarmEntity
{
    public void FoodSubscription(object sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    public void SleepSubscription(IEntity sender, SleepEvent @event)
    {
        Console.WriteLine($"{Name} sleeping");
        if (Name == "Nono") FarmManager.GetInstance().Publish(this, new RIPEvent());
    }

    public void SleepSubscription(IEntity sender, SleepEvent @event, FoodEvent @event2)
    {
        Console.WriteLine($"{Name} sleeping");
        if (Name == "Nono") FarmManager.GetInstance().Publish(this, new RIPEvent());
    }

    public void Subscription(object sender, BroadCastFarmEvent farmEvent)
    {
        throw new NotImplementedException();
    }
}