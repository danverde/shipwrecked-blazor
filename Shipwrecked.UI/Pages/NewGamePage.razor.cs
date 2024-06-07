using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.UI.Models;
using Shipwrecked.UI.Store.Game;
using Shipwrecked.UI.Store.Game.Actions;
using Shipwrecked.UI.Store.Player.Actions;
using Shipwrecked.UI.Store.Settings.Actions;

namespace Shipwrecked.UI.Pages;

/// <summary>
/// Code behind for the New Game Page
/// </summary>
public partial class NewGamePage
{
    #region DI

    [Inject] private ISettingsService SettingsService { get; set; } = default!;
    [Inject] private IState<GameState> GameState { get; set; } = default!;
    [Inject] private IGameService GameService { get; set; } = default!;
    [Inject] private IPlayerService PlayerService { get; set; } = default!;
    [Inject] private IDispatcher Dispatcher { get; set; } = default!;
    [Inject] private NavigationManager NavManager { get; set; } = default!;
    
    #endregion
    
    private const string FormId = "new-game-form";

    private EditContext? FormContext { get; set; }

    private readonly NewGameInput _formInput = new();

    protected override void OnInitialized()
    {
        FormContext = new EditContext(_formInput);
    }
    
    private void HandleFormSubmit()
    {
        Dispatcher.Dispatch(new StartGameAction());
        var settings = SettingsService.GetSettings(_formInput.GameDifficulty);
        var game =  GameService.CreateGame(_formInput.GameDifficulty);
        var player =  PlayerService.CreatePlayer(_formInput.Name, _formInput.Gender, settings);
        Dispatcher.Dispatch(new SetSettingsAction(settings));
        Dispatcher.Dispatch(new SetPlayerAction(player));
        Dispatcher.Dispatch(new GameLoadedAction(game));

        NavManager.NavigateTo($"/game/{GameState.Value.Game.Id}");
    }
}