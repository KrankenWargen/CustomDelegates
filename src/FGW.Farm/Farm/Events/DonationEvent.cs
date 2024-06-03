using FGW.Farm.Farm.Entities.Interfaces;

namespace FGW.Farm.Farm.Events;

public record struct DonationEvent(double Money, string? Note) : IBaseEvent;