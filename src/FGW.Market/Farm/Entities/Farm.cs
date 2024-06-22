using FGW.Web.Farm.Entities.Interfaces;

namespace FGW.Web.Farm.Entities;

public record Farm : IEntity
{
    private static readonly Lazy<Farm> This = new();
    public static Farm GetInstance() => This.Value;
}