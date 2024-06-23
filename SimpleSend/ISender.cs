namespace SimpleSend;

public interface ISender
{
    internal void SubscribeWith(Delegate action);
    public Task Send(params object[] @params);
}