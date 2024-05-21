using System.Diagnostics.CodeAnalysis;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Infrastructure.Models;

/// <summary>
/// Object representing the state of the application as a whole 
/// </summary>
[ExcludeFromCodeCoverage]
public class AppState
{
    public Game Game { get; set; } = default!;
    public Player Player { get; set; } = default!;

    // public Map Map { get; set; }

    // public Inventory Inventory { get; set; }
}