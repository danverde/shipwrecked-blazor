using Fluxor;
using Shipwrecked.Application.Actions;
using Shipwrecked.Domain;
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

    /// <summary>
    /// Reducer used to level up a user 
    /// </summary>
    [ReducerMethod]
    public static PlayerState LevelUpReducer(PlayerState state, LevelUpAction action)
    {
        var player = Util.Clone(state.Player);
        player.Level = action.Level;
        player.Experience = action.Experience;
        player.Stamina = action.Stamina;
        player.MaxStamina = action.MaxStamina;
        player.Health = action.Health;
        player.MaxHealth = action.MaxHealth;
        
        return new PlayerState(player);
    }
    
    /// <summary>
    /// Reducer used to update a players experience 
    /// </summary>
    [ReducerMethod]
    public static PlayerState SetExpReducer(PlayerState state, SetExpAction action) =>
        new PlayerState(Util.Clone(state.Player));

    /// <summary>
    /// Reducer used to update a players stamina 
    /// </summary>
    [ReducerMethod]
    public static PlayerState SetStaminaReducer(PlayerState state, SetStaminaAction action) =>
         new PlayerState(Util.Clone(state.Player));
}