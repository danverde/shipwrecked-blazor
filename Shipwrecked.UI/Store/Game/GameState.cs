using System.Diagnostics.CodeAnalysis;
using Fluxor;
using D = Shipwrecked.Domain.Models;

namespace Shipwrecked.UI.Store.Game;

/// <summary>
/// Object representing the Game State
/// </summary>
[FeatureState]
[ExcludeFromCodeCoverage]
public class GameState : BaseState
{
    public D.Game? Game { get; set; }
    
    /// <summary>
    /// Constructor used for state initialization.
    /// </summary>
    private GameState() {}

    /// <summary>
    /// Public constructor specifying all properties
    /// </summary>
    public GameState(bool loading, bool loaded, D.Game? game)
    {
        Loading = loading;
        Loaded = loaded;
        Game = game;
    }
}