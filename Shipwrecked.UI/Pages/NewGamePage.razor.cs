using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Infrastructure.Interfaces;
using Shipwrecked.UI.Models;
using Shipwrecked.UI.Store.Game;
using Shipwrecked.UI.Store.Game.Actions;
using Shipwrecked.UI.Store.Player.Actions;
using Dispatcher = Fluxor.Dispatcher;

namespace Shipwrecked.UI.Pages;

/// <summary>
/// Code behind for the New Game Page
/// </summary>
public partial class NewGamePage
{
    #region DI

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
        var game =  GameService.StartNewGame(_formInput.GameDifficulty);
        var player =  PlayerService.CreatePlayer(_formInput.Name, _formInput.Gender, _formInput.GameDifficulty);
        Dispatcher.Dispatch(new SetPlayerAction(player));
        Dispatcher.Dispatch(new GameLoadedAction(game));

        // will we have the id in the state yet?
        if (GameState.Value.Game is not null)
            NavManager.NavigateTo($"/game/{GameState.Value.Game.Id}");
    }
}