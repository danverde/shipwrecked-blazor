using Ardalis.GuardClauses;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Shipwrecked.Application.Actions;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Infrastructure.Models;
using Shipwrecked.UI.Services;
using Shipwrecked.UI.Store.Game.Actions;
using Shipwrecked.UI.Store.Player.Actions;

namespace Shipwrecked.UI.Store.Effects;

/// <summary>
/// AppState action side effects
/// </summary>
public class AppStateEffect(IAppStateService appStateService, AlertService alertService, NavigationManager navManager)
{
    private readonly IAppStateService _appStateService = Guard.Against.Null(appStateService);
    private readonly AlertService _alertService = Guard.Against.Null(alertService);
    private readonly NavigationManager _navManager = Guard.Against.Null(navManager);

    [EffectMethod]
    public Task GameOverEffectAsync(GameOverAction action, IDispatcher dispatcher)
    {
        Guard.Against.Null(action);
        Guard.Against.Null(dispatcher);
        
        // TODO need some sort of modal that would freeze the page & handle the navigation
        _alertService.Error("The game ended. You died");
        _navManager.NavigateTo("/");

        return Task.CompletedTask;
    }
    
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
        {
            dispatcher.Dispatch(new SetPlayerAction(appState.Player));   
            dispatcher.Dispatch(new GameLoadedAction(appState.Game));
        }
        else
            _alertService.Error("Unable to load Game");
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
        _alertService.Success("Game Saved");
        dispatcher.Dispatch(new GameLoadedAction(appState.Game));
    }
}