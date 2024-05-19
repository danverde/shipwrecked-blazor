using Fluxor;
using D = Shipwrecked.Domain.Models;

namespace Shipwrecked.UI.Store.Game;

/// <summary>
/// Object representing the Game State
/// </summary>
[FeatureState]
public class GameState
{
    public bool GameLoading { get; set; }
    public bool GameLoaded { get; set; }
    public D.Game? Game { get; set; }
    
    /// <summary>
    /// Constructor used for state initialization.
    /// </summary>
    private GameState() {}

    /// <summary>
    /// Public constructor specifying all properties
    /// </summary>
    public GameState(bool gameLoading, bool gameLoaded, D.Game? game)
    {
        GameLoading = gameLoading;
        GameLoaded = gameLoaded;
        Game = game;
    }
}