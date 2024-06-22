using FGW.Web.Farm.Entities.Interfaces;
using SimpleSend;

namespace FGW.Web.Farm.Events;

public struct BroadCastFarmEvent(IResource Resource) : IBaseEvent;