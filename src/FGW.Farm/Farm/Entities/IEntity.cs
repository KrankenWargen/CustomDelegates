using FGW.Core.Farm.Events;

namespace FGW.Core.Farm.Entities;

public interface IEntity
{
    protected void Subscription(IEntity sender, BroadCastEvent @event);
}