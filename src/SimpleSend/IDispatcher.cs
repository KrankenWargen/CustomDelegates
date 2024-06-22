namespace FGW.Farm;

public interface IDispatcher
{
    public void SubscribeWith(Delegate action);
    public void Send(params object[] @params);
}