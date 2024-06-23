using FGW.Web.Farm.Entities.Interfaces;
using FGW.Web.Farm.Events;
using SimpleSend;

namespace FGW.Web.Farm.Entities.Animals;

public record Cat(IOrchestrate Orchestrate) : IFarmEntity
{

    private List<Cat> cats = [];
    private int Age = new Random().Next(12, 27);
    private string Name;

    public void Create(object sender, CreateEvent @event)
    {
        cats.Add(new Cat(Orchestrate)
        {
            Name = @event.Name
        });
    }

    public void FoodSubscription(object sender, FoodEvent @event)
    {
        Console.WriteLine("Cat food received!!");
    }

    public void SleepSubscription(IEntity sender, SleepEvent @event)
    {
        Console.WriteLine($"{Name} sleeping");
        if (Name == "Nono") Orchestrate.Send(this, new RIPEvent());
    }

    public void Subscription(object sender, BroadCastFarmEvent farmEvent)
    {
        throw new NotImplementedException();
    }
}