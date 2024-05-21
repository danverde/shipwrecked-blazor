using System.Diagnostics.CodeAnalysis;
using Fluxor;
using D = Shipwrecked.Domain.Models;

namespace Shipwrecked.UI.Store.Player;

/// <summary>
/// Object representing the Player state
/// </summary>
[FeatureState]
[ExcludeFromCodeCoverage]
public class PlayerState
{
    public D.Player? Player { get; set; }

    /// <summary>
    /// Constructor used for state initialization.
    /// </summary>
    private PlayerState() {}

    public PlayerState(D.Player? player)
    {
        Player = player;
    }
}