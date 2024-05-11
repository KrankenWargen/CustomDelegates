using CustomDelegates;
using CustomDelegates.Models;

namespace CustomDelegates;

class Play
{
    delegate int PerformCalculation(int x, int y);

    int oxut => DispatchAfterAdding((x, y) => x + y);

    private int x = 2;
    private int y = 3;

    private int DispatchAfterAdding(Func<int, int, int> catAction)
    {
        return catAction(x, y);
    }
}

public delegate void SampleEventDispatcher(object sender, IEnumerable<int> ballCount);

public delegate T AddToCounter<T>(T animal);

public class Program
{
    private static void InitialDispatch(AddToCounter<Animal> animalAdding, Animal animal) =>
        animalAdding(animal).Trigger();

    private static event SampleEventDispatcher? Dispatcher;

    public static void Main(string[] args)
    {
        var animal = new Animal(new List<Animal>
        {
            new Cat(),
            new Dog()
        }, Dispatcher);
        InitialDispatch(cat =>
        {
            var x = cat.BallCount.ToList();
            x.Add(1);
            return cat with
            {
                BallCount = x.AsReadOnly()
            };
        }, animal);
    }
}