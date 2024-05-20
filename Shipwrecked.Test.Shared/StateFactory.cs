using Shipwrecked.UI.Store.Game;

namespace Shared;

/// <summary>
/// Helper class used to generate state objects for unit tests
/// </summary>
public static class StateFactory
{
    /// <summary>
    /// Get a basic GameState object
    /// </summary>
    public static GameState GetGameState()
    {
        return new GameState(false, true, DomainFactory.CreateGame());
    } 
}