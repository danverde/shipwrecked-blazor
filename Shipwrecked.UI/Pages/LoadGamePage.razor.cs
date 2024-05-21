using Fluxor;
using Microsoft.AspNetCore.Components;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Infrastructure.Models;
using Shipwrecked.UI.Store.Game.Actions;

namespace Shipwrecked.UI.Pages;

/// <summary>
/// Code behind for Load Game page
/// </summary>
public partial class LoadGamePage
{
    [Inject] private NavigationManager NavManager { get; set; } = default!;

    [Inject] private IAppStateService AppStateService { get; set; } = default!;

    [Inject] private IDispatcher Dispatcher { get; set; } = default!;
    
    private IList<AppState> States { get; set; } = new List<AppState>();
    
    protected override async Task OnInitializedAsync()
    {
        States = await AppStateService.ListAsync();
        await base.OnInitializedAsync();
    }

    private void HandleLoadGameClick(Guid id)
    {
        Dispatcher.Dispatch(new LoadGameAction(id));
        
        // TODO do I navigate here, or as an effect?
        NavManager.NavigateTo($"/game/{id}");
    }
}