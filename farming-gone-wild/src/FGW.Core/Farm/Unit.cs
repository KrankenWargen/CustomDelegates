namespace FGW.Core.Farm;

public struct Unit : IEquatable<Unit>
{
  
    public static readonly Unit Default = new();
    public bool Equals(Unit other) => true;
    public override bool Equals(object? obj) => obj is Unit;
    public override int GetHashCode() => 0;
    public static bool operator ==(Unit left, Unit right) => true;
    public static bool operator !=(Unit left, Unit right) => false;
}