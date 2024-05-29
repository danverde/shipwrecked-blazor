using Fluxor;
using Newtonsoft.Json;
using Shipwrecked.Application.Actions;
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
    /// TODO fix this so it's not a copy of the set player reducer...
    [ReducerMethod]
    public static PlayerState LevelUpReducer(PlayerState state, LevelUpAction action) =>
        new PlayerState(action.Player);

    /// <summary>
    /// Reducer used to update a players stamina 
    /// </summary>
    [ReducerMethod]
    public static PlayerState SetStaminaReducer(PlayerState state, SetStaminaAction action) =>
         new PlayerState(ClonePlayer(state.Player!));
    
    /// <summary>
    /// Reducer used to update a players experience 
    /// </summary>
    [ReducerMethod]
    public static PlayerState SetExpReducer(PlayerState state, SetExpAction action) =>
        new PlayerState(ClonePlayer(state.Player!));

    
    
    /// <summary>
    /// Helper method used to create a deep clone of a player object
    /// </summary>
    private static Domain.Models.Player ClonePlayer(Domain.Models.Player player)
    {
        return JsonConvert.DeserializeObject<Domain.Models.Player>(JsonConvert.SerializeObject(player)) ?? new Domain.Models.Player();
    }
}