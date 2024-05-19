using FGW.Core.Farm.Resources;

namespace FGW.Core.Farm.Events;

public struct BroadCastEvent(IResource Resource) : IBaseEvent;