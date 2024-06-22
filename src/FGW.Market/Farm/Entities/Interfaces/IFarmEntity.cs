using FGW.Web.Farm.Events;

namespace FGW.Web.Farm.Entities.Interfaces;

public interface IFarmEntity : IEntity
{
    protected void Subscription(object sender, BroadCastFarmEvent farmEvent);
}