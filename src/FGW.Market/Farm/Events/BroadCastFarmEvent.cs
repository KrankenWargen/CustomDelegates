using FGW.Farm;
using FGW.Web.Farm.Entities.Interfaces;

namespace FGW.Web.Farm.Events;

public struct BroadCastFarmEvent(IResource Resource) : IBaseEvent;