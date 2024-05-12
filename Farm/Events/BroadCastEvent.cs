using CustomDelegates.Farm.Resources;

namespace CustomDelegates.Farm.Events;

public struct BroadCastEvent(IResource Resource) : IBaseEvent;