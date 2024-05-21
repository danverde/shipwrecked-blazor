using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.Domain.Models;

/// <summary>
/// A location on a map
/// </summary>
[ExcludeFromCodeCoverage]
public class MapLocation : Location
{
    public Scene Scene { get; set; } = default!;
    public Guid? CharacterId { get; set; }
}