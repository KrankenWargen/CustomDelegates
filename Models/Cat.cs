namespace CustomDelegates.Models;

public class Cat : Animal
{
    private SampleEventDispatcher? _dispatcher;

    private void TriggerUpdate()
    {
        Console.WriteLine($"Update: {BallCount.Last()}");
        _dispatcher?.Invoke(this, BallCount);
    }

    public void Subscriber(object sender, IEnumerable<int> ballCount)
    {
        if (sender is (Animal or Dog) and not Cat)
        {
            var newList = ballCount.ToList();
            newList.Add(sender is Dog ? ballCount.TakeLast(2).Sum() : 1);

            BallCount = newList;
            if (BallCount.Count() <= 16) TriggerUpdate();
        }
    }

    public void setDispatcher(SampleEventDispatcher? dispatcher)
    {
        _dispatcher = dispatcher;
    }
}