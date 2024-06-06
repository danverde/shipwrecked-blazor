using Fluxor;
using Shipwrecked.Application.Actions;
using Shipwrecked.Domain;
using Shipwrecked.UI.Store.Game.Actions;

namespace Shipwrecked.UI.Store.Game;

/// <summary>
/// Reducers for the Game State
/// </summary>
public static class GameReducer
{
    /// <summary>
    /// Reducer for the <see cref="LoadGameAction"/> action.
    /// </summary>
    [ReducerMethod]
    public static GameState LoadGameReducer(GameState state, LoadGameAction action) =>
        new(loaded: false, loading: true, game: null);

    /// <summary>
    /// Reducer for the <see cref="StartGameAction"/> action.
    /// </summary>
    [ReducerMethod]
    public static GameState StartGameReducer(GameState state, StartGameAction action) =>
        new GameState(true, false, null);
    
    /// <summary>
    /// Reducer for the <see cref="GameLoadedAction"/> action.
    /// </summary>
    [ReducerMethod]
    public static GameState GameLoadedReducer(GameState state, GameLoadedAction action) =>
        new GameState(false, true, action.Game);

    /// <summary>
    /// Reducer for the <see cref="QuitGameAction"/> action.
    /// </summary>
    [ReducerMethod]
    public static GameState QuitGameReducer(GameState state, QuitGameAction action) =>
        new GameState(false, false, new Domain.Models.Game());
    
    /// <summary>
    /// Reducer for the <see cref="GameOverAction"/> action.
    /// </summary>
    [ReducerMethod]
    public static GameState GameOverReducer(GameState state, GameOverAction action) =>
        new GameState(false, false, new Domain.Models.Game());
    
    /// <summary>
    /// Reducer for the <see cref="IncrementDayAction"/> action.
    /// </summary>
    [ReducerMethod]
    public static GameState IncrementDayReducer(GameState state, IncrementDayAction action)
    {
        var newState = Util.Clone(state);
        newState.Game.Day = action.Day;
        return newState;
    }
}