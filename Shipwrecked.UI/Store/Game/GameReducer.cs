using Fluxor;
using Shipwrecked.Application.Actions;

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
        new(gameLoaded: false, gameLoading: true, game: null);

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
        new GameState(false, false, null);
    
    [ReducerMethod]
    public static GameState IncrementDayReducer(GameState state, IncrementDayAction action)
    {
        // TODO come up with a deep clone method for the game?
        var newState = new GameState(state.GameLoading, state.GameLoaded, state.Game);
        if (newState.Game is not null)
            newState.Game!.Day = action.Day;
        return newState;
    }
}