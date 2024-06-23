using FGW.Web.Farm.Entities.Interfaces;
using FGW.Web.Farm.Events;
using SimpleSend;

namespace FGW.Web.Farm.Entities.Animals;

public record Cat(ISender Sender) : IEntity
{

    private List<Cat> cats = [];
    private int Age = new Random().Next(12, 27);
    private string Name;

    public void Create(object sender, CreateEvent @event)
    {
        cats.Add(new Cat(Sender)
        {
            Name = @event.Name
        });
    }
    
    public void SleepSubscription(IEntity sender, SleepEvent @event)
    {
        Console.WriteLine($"{Name} sleeping");
    }

    public async Task SleepSubscription(Dog sender, SleepEvent @event)
    {
        Console.WriteLine($"{Name} sleeping async");
    }
}