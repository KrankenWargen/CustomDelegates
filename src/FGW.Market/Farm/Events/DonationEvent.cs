using SimpleSend;

namespace FGW.Web.Farm.Events;

public record struct DonationEvent(double Money, string? Note) : IBaseEvent;    