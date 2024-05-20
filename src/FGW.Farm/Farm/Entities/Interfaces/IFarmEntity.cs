using FGW.Core.Farm.Events;

namespace FGW.Core.Farm.Entities.Interfaces;

public interface IFarmEntity : IEntity
{
    protected void Subscription(IEntity sender, BroadCastFarmEvent farmEvent);
}