using Fluxor;
using Shipwrecked.Application.Actions;

namespace Shipwrecked.UI.Store.Game;

/// <summary>
/// Reducer for the Game State
/// </summary>
public class GameReducer
{
    /// <summary>
    /// Reducer for the <see cref="LoadGameAction"/> action.
    /// </summary>
    [ReducerMethod]
    public static GameState LoadGameReducer(GameState state, LoadGameAction action) =>
        new(gameLoaded: false, gameLoading: true, game: null);

    /// <summary>
    /// Reducer for the <see cref="GameLoadedAction"/> action.
    /// </summary>
    [ReducerMethod]
    public static GameState GameLoadedReducer(GameState state, GameLoadedAction action) =>
        new GameState(false, true, action.Game);

    [ReducerMethod]
    public static GameState QuitGameReducer(GameState state, QuitGameAction action) =>
        new GameState(false, false, null);
}