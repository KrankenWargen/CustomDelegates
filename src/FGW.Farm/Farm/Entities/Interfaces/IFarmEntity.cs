using FGW.Farm.Farm.Events;

namespace FGW.Farm.Farm.Entities.Interfaces;

public interface IFarmEntity : IEntity
{
    protected void Subscription(object sender, BroadCastFarmEvent farmEvent);
}