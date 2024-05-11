using CustomDelegates.Farm.Events;
using CustomDelegates.Infrastructure;

namespace CustomDelegates.Farm.Entities;

public interface IEntity
{
    public void Register(IEntity entity)
    {
        EventManager.SubscribeWith(entity);
    }

    public void Leave(IEntity entity)
    {
        EventManager.UnsubscribeFor(entity);
    }

    public void Subscriber(object sender, IBaseEvent @event);
}