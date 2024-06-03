using FGW.Farm.Farm.Entities.Interfaces;

namespace FGW.Farm.Entities;

public record Farm : IEntity
{
    private static readonly Lazy<Farm?> @this = new Lazy<Farm?>();

    public static Farm? GetInstance() => @this.Value;
}