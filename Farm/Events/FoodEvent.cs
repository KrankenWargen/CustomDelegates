using CustomDelegates.Farm.Resources;

namespace CustomDelegates.Farm.Events;

public struct FoodEvent(IFood Food) : IBaseEvent;