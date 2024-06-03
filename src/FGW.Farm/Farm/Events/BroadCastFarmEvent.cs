using FGW.Farm.Farm.Entities.Interfaces;

namespace FGW.Farm.Farm.Events;

public struct BroadCastFarmEvent(IResource Resource) : IBaseEvent;