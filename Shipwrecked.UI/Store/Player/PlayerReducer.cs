using Fluxor;
using Shipwrecked.UI.Store.Player.Actions;

namespace Shipwrecked.UI.Store.Player;

/// <summary>
/// Reducers for the player state
/// </summary>
public static class PlayerReducer
{
    /// <summary>
    /// Reducer used to set player state after initial player setup,
    /// either during new game creation or loading of an existing game
    /// </summary>
    [ReducerMethod]
    public static PlayerState SetPlayerReducer(PlayerState state, SetPlayerAction action) =>
        new PlayerState(action.Player);
}