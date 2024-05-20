using Ardalis.GuardClauses;
using Fluxor;
using Shipwrecked.Application.Actions;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Infrastructure.Models;

namespace Shipwrecked.UI.Store.Effects;

/// <summary>
/// Game action side effects
/// </summary>
public class GameEffect(IAppStateService appStateService)
{
    private readonly IAppStateService _appStateService = Guard.Against.Null(appStateService);

    /// <summary>
    /// Side effect that handles loading a saved game
    /// </summary>
    [EffectMethod]
    public async Task LoadGameEffectAsync(LoadGameAction action, IDispatcher dispatcher)
    {
        AppState appState = await _appStateService.LoadAsync(action.Id);
        dispatcher.Dispatch(new GameLoadedAction(appState.Game));
    }

    /// <summary>
    /// Side effects from saving a game
    /// </summary>
    [EffectMethod]
    public async Task SaveGameEffectAsync(SaveGameAction action, IDispatcher dispatcher)
    {
        AppState appState = await _appStateService.SaveAsync(action.Game.Id, action);
        dispatcher.Dispatch(new GameLoadedAction(appState.Game));
    }
}