using Microsoft.AspNetCore.Components;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure;
using Shipwrecked.Infrastructure.Interfaces;

namespace Shipwrecked.UI.Pages;

public partial class Game
{
    [Parameter]
    public string? Id { get; set; }

    [Inject] 
    private NavigationManager NavigationManager { get; set; } = default!;
    
    [Inject] 
    private IGameService GameService { get; set; } = default!;

    [Inject] 
    private IReadContext Context { get; set; } = default!;

    [Inject] 
    private IStateStorage StateStorage { get; set; } = default!;

    private Domain.Models.Game? GameState { get; set; }

    private Player? PlayerState { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // Load game from local storage (if possible)
        await LoadGameById(Id);
        
        // load state from context & setup local vars
        State state = Context.GetState();
        GameState = state.Game;
        PlayerState = state.Player;
        
        await base.OnInitializedAsync();
    }

    private async Task LoadGameById(string? stringId)
    {
        bool validId = Guid.TryParse(stringId, out Guid id); 
        if (!validId)
        {
            NavigationManager.NavigateTo("/");
        }
        else if (validId && await StateStorage.StateExistsAsync(id))
        {
            await StateStorage.LoadStateAsync(id);
        }
    }

    private void IncrementDay()
    {
        Console.WriteLine("Increment Day called");
        
        // GameState.Day++; // TODO only updated the game instance for this component... YIKES!
        GameService.IncrementDay();
    }

    private void SaveGame()
    {
        Console.WriteLine("Save Game called!");
        StateStorage.SaveStateAsync(GameState.Id, Context.GetState());
    }
}