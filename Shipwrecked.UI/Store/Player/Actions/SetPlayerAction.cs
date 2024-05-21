using System.Diagnostics.CodeAnalysis;
using D = Shipwrecked.Domain.Models;

namespace Shipwrecked.UI.Store.Player.Actions;

/// <summary>
/// Action triggered when setting the initial player state,
/// either while generating a new game, or when loading an existing game.
/// </summary>
[ExcludeFromCodeCoverage]
public class SetPlayerAction
{
    public D.Player? Player { get; set; }
}