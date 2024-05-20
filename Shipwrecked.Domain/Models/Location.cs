using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.Domain.Models;

/// <summary>
/// A single location object
/// </summary>
[ExcludeFromCodeCoverage]
public class Location
{
    public int X { get; set; }
    public int Y { get; set; }
    public Scene Scene { get; set; }
    public Guid? CharacterId { get; set; }
}