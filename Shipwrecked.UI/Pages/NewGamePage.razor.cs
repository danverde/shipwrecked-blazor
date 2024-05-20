using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Infrastructure.Interfaces;
using Shipwrecked.UI.Models;
using Shipwrecked.UI.Store.Game;

namespace Shipwrecked.UI.Pages;

/// <summary>
/// Code behind for the New Game Page
/// </summary>
public partial class NewGamePage
{
    #region DI

    [Inject] private IState<GameState> GameState { get; set; } = default!;
    
    [Inject] private IAppStateStore AppStateStore { get; set; } = default!;
    
    [Inject] private IGameService GameService { get; set; } = default!;
    
    // [Inject] private IPlayerService PlayerService { get; set; } = default!;

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
        // TODO setting up the character should be part of loading the game!
        GameService.StartNewGame(_formInput.GameDifficulty);
        // PlayerService.CreatePlayer(_formInput.Name, _formInput.Gender, _formInput.GameDifficulty);

        // will we have the id in the state yet?
        if (GameState.Value.Game is not null)
            NavManager.NavigateTo($"/game/{GameState.Value.Game.Id}");
    }
}