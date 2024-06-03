using FGW.Farm.Farm.Entities.Interfaces;

namespace FGW.Farm.Farm.Events;

public struct FoodEvent(IFood Food) : IBaseEvent;