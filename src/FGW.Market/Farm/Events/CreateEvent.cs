using SimpleSend;

namespace FGW.Web.Farm.Events;

public struct CreateEvent(string name) : IBaseEvent
{
    public readonly string Name = name;
}