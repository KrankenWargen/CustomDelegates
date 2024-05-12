using CustomDelegates.Farm.Events;

namespace CustomDelegates.Farm.Entities;

public interface IEntity
{
    protected void Subscription(IEntity sender, BroadCastEvent @event);
}