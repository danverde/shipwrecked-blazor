using Microsoft.AspNetCore.Components;
using Shipwrecked.Infrastructure;
using Shipwrecked.Infrastructure.Interfaces;

namespace Shipwrecked.UI.Pages;

/// <summary>
/// Code behind for Load Game page
/// </summary>
public partial class LoadGame
{
    [Inject] private IContext Context { get; set; } = default!;
    
    [Inject] private NavigationManager NavManager { get; set; } = default!;

    [Inject] private IStateStorage StateStorage { get; set; } = default!;

    private IList<State> States { get; set; } = new List<State>();
    
    protected override async Task OnInitializedAsync()
    {
        States = await StateStorage.ListSavedStatesAsync();
        await base.OnInitializedAsync();
    }

    private async Task HandleLoadGameClickAsync(Guid id)
    {
        State state = await StateStorage.LoadStateAsync(id);
        Context.SetState(state);
        NavManager.NavigateTo($"/game/{id}");
    }
}