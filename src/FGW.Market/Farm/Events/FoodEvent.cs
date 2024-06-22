using FGW.Web.Farm.Entities.Interfaces;
using SimpleSend;

namespace FGW.Web.Farm.Events;

public struct FoodEvent(IFood Food) : IBaseEvent;