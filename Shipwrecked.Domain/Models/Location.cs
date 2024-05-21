using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.Domain.Models;

/// <summary>
/// Basic location data
/// </summary>
[ExcludeFromCodeCoverage]
public class Location
{
    public int X { get; set; }
    public int Y { get; set; }
}