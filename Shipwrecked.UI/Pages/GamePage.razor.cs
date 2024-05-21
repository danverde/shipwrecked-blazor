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

    protected override void OnInitialized()
    {
        var idIsValid = Guid.TryParse(Id, out Guid gameId);
        var gameLoaded = GameState.Value.Loaded;
        var game = GameState.Value.Game;
        
        if (!gameLoaded && !idIsValid)
        {
            NavManager.NavigateTo("/");
        } else if (idIsValid && !gameLoaded)
        {
            Dispatcher.Dispatch(new LoadGameAction(gameId));
        } else if (gameLoaded && (!idIsValid || game!.Id != gameId))
        {
            NavManager.NavigateTo($"/game/{game!.Id}");
        }
    }

    private void HandleWaitClick()
    {
        if (GameState.Value.Game is not null)
            Dispatcher.Dispatch(new IncrementDayAction(GameState.Value.Game.Day + 1));
    }

    private void HandleMenuClick()
    {
        throw new NotImplementedException();
    }

    private void SaveGame()
    {
        // TODO figure out null handling, cuz this is gonna get out of hand quickly...
        if (GameState.Value.Game is not null && PlayerState.Value.Player is not null)
            Dispatcher.Dispatch(new SaveGameAction(GameState.Value.Game, PlayerState.Value.Player));
    }
}