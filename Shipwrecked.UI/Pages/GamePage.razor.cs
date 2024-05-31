using Fluxor;
using Microsoft.AspNetCore.Components;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.UI.Store.Game;
using Shipwrecked.UI.Store.Game.Actions;
using Shipwrecked.UI.Store.Player;

namespace Shipwrecked.UI.Pages;

public partial class GamePage
{
    [Parameter]
    public string? Id { get; set; }
    
    [Inject] private NavigationManager NavManager { get; set; } = default!;
    
    [Inject] private IGameService GameService { get; set; } = default!;

    [Inject] private IState<GameState> GameState { get; set; } = default!;
    [Inject] private IState<PlayerState> PlayerState { get; set; } = default!;

    [Inject] private IDispatcher Dispatcher { get; set; } = default!;

    private bool MenuIsOpen { get; set; }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        var idIsValid = Guid.TryParse(Id, out Guid gameId);
        var gameLoaded = GameState.Value.Loaded;
        var game = GameState.Value.Game;
        
        if (!gameLoaded && !idIsValid)
        {
            NavManager.NavigateTo("/");
        } else if (idIsValid && !gameLoaded)
        {
            Dispatcher.Dispatch(new LoadGameAction(gameId));
        } else if (gameLoaded && (!idIsValid || game.Id != gameId))
        {
            NavManager.NavigateTo($"/game/{game.Id}");
        }
    }

    private void OpenMenu()
    {
        MenuIsOpen = true;
    }
    
    private void Wait()
    {
        Dispatcher.Dispatch(new IncrementDayAction(GameState.Value.Game.Day + 1));
    }

    private void QuitGame()
    {
        Dispatcher.Dispatch(new QuitGameAction());
        NavManager.NavigateTo("/");
    }

    private void SaveGame()
    {
        Dispatcher.Dispatch(new SaveGameAction(GameState.Value.Game, PlayerState.Value.Player));
    }
}