namespace CustomDelegates.Models;

public class Animal : IAnimal
{
    private readonly IEnumerable<Animal> _animals = new List<Animal>();
    private SampleEventDispatcher? _dispatcher;

    public Animal(IEnumerable<Animal> animals, SampleEventDispatcher? dispatcher)
    {
        _animals = animals;
        _dispatcher = dispatcher;
    }

    public Animal()
    {
    }

    public IEnumerable<int> BallCount { get; set; } = new List<int>();

    private void SubscribeAnimals()
    {
        foreach (var animal in _animals)
        {
            switch (animal)
            {
                case Cat cat:
                    _dispatcher += cat.Subscriber;
                    break;
                case Dog dog:
                    _dispatcher += dog.Subscriber;
                    break;
            }
        }
    }

    private void PassDispatcher()
    {
        foreach (var animal in _animals)
        {
            switch (animal)
            {
                case Cat cat:
                    cat.setDispatcher(_dispatcher);
                    break;
                case Dog dog:
                    dog.setDispatcher(_dispatcher);
                    break;
            }
        }
    }

    public void Trigger()
    {
        if (_dispatcher is null)
        {
            SubscribeAnimals();
            PassDispatcher();
        }

        _dispatcher?.Invoke(this, BallCount);
    }

    public virtual void Subscriber(object sender, IEnumerable<int> ballCount)
    {
    }
}