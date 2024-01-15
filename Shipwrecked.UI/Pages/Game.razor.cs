using Microsoft.AspNetCore.Components;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure;
using Shipwrecked.Infrastructure.Interfaces;

namespace Shipwrecked.UI.Pages;

public partial class Game
{
    [Parameter]
    public string Id { get; set; }

    [Inject] 
    private IGameService GameService { get; set; } = default!;

    [Inject] 
    private IContext Context { get; set; } = default!;

    [Inject] 
    private IStateStorage StateStorage { get; set; } = default!;

    private Domain.Models.Game GameState { get; set; } = default!;

    private Player PlayerState { get; set; } = default!;
    
    protected override void OnInitialized()
    {
        State state = Context.GetState();
        GameState = state.Game;
        PlayerState = state.Player;
        
        // TODO on page load look for a game with the corresponding ID in state...
        base.OnInitialized();
    }

    private void IncrementDay()
    {
        Console.WriteLine("Increment Day called");

        GameState.Day++; // TODO only updated the game instance for this component... YIKES!

        // GameService.IncrementDay();
    }

    private void SaveGame()
    {
        Console.WriteLine("Save Game called!");
        StateStorage.SaveStateAsync(GameState.Id, Context.GetState());
    }
}