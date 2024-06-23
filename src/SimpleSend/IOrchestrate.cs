namespace SimpleSend;

public interface IOrchestrate
{
    internal void SubscribeWith(Delegate action);
    public void Send(params object[] @params);
}