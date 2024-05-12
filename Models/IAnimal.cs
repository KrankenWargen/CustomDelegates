namespace CustomDelegates.Models;

public interface IAnimal
{
    public void Trigger();
    public void Subscriber(object sender,IEnumerable<int> ballCount);
}