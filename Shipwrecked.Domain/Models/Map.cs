using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.Domain.Models;

[ExcludeFromCodeCoverage]
public class Map
{
    public List<MapLocation> Locations { get; set; } = new();
    
    public bool EnableFog { get; set; }
}