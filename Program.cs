using CustomDelegates.Farm.Entities;
using CustomDelegates.Farm.Events;
using CustomDelegates.Farm.Resources;
using CustomDelegates.Infrastructure;

namespace CustomDelegates;

public class Program
{
    private delegate void AddToFarm<in T>(IEnumerable<T> animals);

    private static void InitialDispatch(AddToFarm<IEntity> addAll, IEnumerable<IEntity> animals) => addAll(animals);

    public static void Main(string[] args)
    {
        InitialDispatch(animals => EventManager.SubscribeWith(animals.ToList()), new List<IEntity>
        {
            new Cat(),
            new Dog()
        });
        EventManager.Publish(new Dog(), new FoodEvent(new Milk()));
    }
}