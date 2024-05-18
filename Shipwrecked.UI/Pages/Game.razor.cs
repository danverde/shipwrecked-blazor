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
    private NavigationManager NavManager { get; set; } = default!;
    
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
        
        // nav to home screen if the context is empty after pulling from storage
        if (GameState == null || PlayerState == null)
            NavManager.NavigateTo("/`");
        
        await base.OnInitializedAsync();
    }

    private async Task LoadGameById(string? stringId)
    {
        bool validId = Guid.TryParse(stringId, out Guid id);
        if (validId && await StateStorage.StateExistsAsync(id))
        {
            // TODO causes race condition w/ init method on other components! need a way to subscribe to the state!
            // TODO or could I use the sync version of the local storage service? Seems like a subscription would still be the way to go...
            Console.WriteLine("loading state from local storage");
            await StateStorage.LoadStateAsync(id);
        }
        else if (!validId)
        {
            NavManager.NavigateTo("/");
        }
    }
    
    private void HandleWaitClick()
    {
        GameService.IncrementDay();
    }

    private void HandleMenuClick()
    {
        
    }

    private void SaveGame()
    {
        Console.WriteLine("Save Game called!");
        StateStorage.SaveStateAsync(GameState.Id, Context.GetState());
    }
}