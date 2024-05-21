using Ardalis.GuardClauses;
using Fluxor;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Infrastructure.Models;
using Shipwrecked.UI.Store.Game.Actions;

namespace Shipwrecked.UI.Store.Effects;

/// <summary>
/// Game action side effects
/// TODO switch actions to load/save appState?
/// </summary>
public class GameEffect(IAppStateService appStateService)
{
    private readonly IAppStateService _appStateService = Guard.Against.Null(appStateService);

    /// <summary>
    /// Side effect that handles loading a saved game
    /// </summary>
    [EffectMethod]
    public async Task LoadAppStateEffectAsync(LoadGameAction action, IDispatcher dispatcher)
    {
        Guard.Against.Null(action);
        Guard.Against.Null(dispatcher);
        
        AppState? appState = await _appStateService.LoadAsync(action.Id);
        if (appState is not null)
            dispatcher.Dispatch(new GameLoadedAction(appState.Game));
        else
            throw new NotImplementedException("Need to implement failure actions!");
    }

    /// <summary>
    /// Side effects from saving a game
    /// </summary>
    [EffectMethod]
    public async Task SaveAppStateEffectAsync(SaveGameAction action, IDispatcher dispatcher)
    {
        Guard.Against.Null(action);
        Guard.Against.Null(dispatcher);
        
        AppState appState = await _appStateService.SaveAsync(action.Game, action.Player);
        dispatcher.Dispatch(new GameLoadedAction(appState.Game));
    }
}