using FGW.Core.Farm.Resources;

namespace FGW.Core.Farm.Events;

public struct FoodEvent(IFood Food) : IBaseEvent;