using CustomDelegates.Farm.Resources;

namespace CustomDelegates.Farm.Events;

public record FoodEvent(IFood Resource) : IBaseEvent;