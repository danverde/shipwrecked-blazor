using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.Domain.Models;

[ExcludeFromCodeCoverage]
public class Map
{
    public List<Location> Locations { get; set; }
    
    public bool EnableFog { get; set; }
}