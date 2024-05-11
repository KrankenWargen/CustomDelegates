namespace CustomDelegates.Models;

public record Dog : Animal
{
    private SampleEventDispatcher? _dispatcher;

    private void TriggerUpdate(Dog newDog)
    {
        Console.WriteLine($"Update: {newDog.BallCount.Last()}");
        _dispatcher?.Invoke(this, newDog.BallCount);
    }

    public override void Subscriber(object sender, IEnumerable<int> ballCount)
    {
        if (sender is Cat)
        {
            var newDog = this with
            {
                BallCount = ballCount.Concat(new[] { ballCount.TakeLast(2).Sum()}).ToList(),
            };
            TriggerUpdate(newDog);
        }
    }

    public void setDispatcher(SampleEventDispatcher? dispatcher)
    {
        _dispatcher = dispatcher;
    }
}