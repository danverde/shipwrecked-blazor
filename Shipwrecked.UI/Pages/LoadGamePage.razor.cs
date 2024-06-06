using Fluxor;
using Microsoft.AspNetCore.Components;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Infrastructure.Models;
using Shipwrecked.UI.Services;
using Shipwrecked.UI.Store.Game.Actions;

namespace Shipwrecked.UI.Pages;

/// <summary>
/// Code behind for Load Game page
/// </summary>
public partial class LoadGamePage
{
    [Inject] private NavigationManager NavManager { get; set; } = default!;
    [Inject] private IAlertService AlertService { get; set; } = default!;
    [Inject] private IAppStateService AppStateService { get; set; } = default!;
    [Inject] private IDispatcher Dispatcher { get; set; } = default!;
    
    private IList<AppState> States { get; set; } = new List<AppState>();
    
    protected override async Task OnInitializedAsync()
    {
        States  = await AppStateService.ListAsync();
        await base.OnInitializedAsync();
    }

    private void HandleLoadGameClick(Guid id)
    {
        Dispatcher.Dispatch(new LoadGameAction(id));
        NavManager.NavigateTo($"/game/{id}");
    }

    private async Task DeleteSaveGameAsync(Guid gameId, string playerName)
    {
        await AppStateService.DeleteAsync(gameId);
        States = await AppStateService.ListAsync();
        AlertService.Success($"{playerName} Deleted");
    }
}