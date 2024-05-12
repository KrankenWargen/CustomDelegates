namespace CustomDelegates.Models;

public record Cat : Animal
{
    private SampleEventDispatcher? _dispatcher;

    private void TriggerUpdate(Cat cat)
    {
        Console.WriteLine($"Update: {cat.BallCount.Last()}");
        _dispatcher?.Invoke(this, cat.BallCount);
    }

    public void Subscriber(object sender, IEnumerable<int> ballCount)
    {
        if (sender.GetType() == typeof(Animal) || sender is Dog)
        {
            var newCat = this with
            {
                BallCount = ballCount.Concat(new[] { sender is Dog ? ballCount.TakeLast(2).Sum() : 1 }).ToList(),
            };
            if (newCat.BallCount.Count() <= 16) TriggerUpdate(newCat);
        }
    }

    public void setDispatcher(SampleEventDispatcher? dispatcher)
    {
        _dispatcher = dispatcher;
    }
}