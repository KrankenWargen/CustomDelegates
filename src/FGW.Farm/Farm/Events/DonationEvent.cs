using FGW.Core.Farm.Entities.Interfaces;

namespace FGW.Core.Farm.Events;

public record struct DonationEvent(double Money, string? Note) : IBaseEvent;