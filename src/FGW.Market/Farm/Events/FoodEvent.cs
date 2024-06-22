using FGW.Farm;
using FGW.Web.Farm.Entities.Interfaces;

namespace FGW.Web.Farm.Events;

public struct FoodEvent(IFood Food) : IBaseEvent;