namespace CustomDelegates.Models;

public class Dog : Animal
{
    private SampleEventDispatcher? _dispatcher;

    private void TriggerUpdate()
    {
        Console.WriteLine($"Update: {BallCount.Last()}");
        _dispatcher?.Invoke(this, BallCount);
    }

    public override void Subscriber(object sender, IEnumerable<int> ballCount)
    {
        if (sender is Cat)
        {
            var newList = ballCount.ToList();
            newList.Add(ballCount.TakeLast(2).Sum());

            BallCount = newList;
            TriggerUpdate();
        }
    }

    public void setDispatcher(SampleEventDispatcher? dispatcher)
    {
        _dispatcher = dispatcher;
    }
}