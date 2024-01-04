using Microsoft.AspNetCore.Components;
using Shipwrecked.UI.Models;
using Shipwrecked.UI.Services;

namespace Shipwrecked.UI.Components;

/// <summary>
/// Code behind for the Drawer Component
/// </summary>
public partial class Drawer : IDisposable
{
    /// <summary>
    /// Drawer Service used to consume & dispatch drawer events
    /// </summary>
    [Inject] private IDrawerService DrawerService { get; set; }

    #region Input Parameters

    /// <summary>
    /// The Id of the drawer.
    /// </summary>
    [Parameter] public DrawerId Id { get; set; }
    
    /// <summary>
    /// Optional Title of the drawer
    /// </summary>
    [Parameter] public string? Title { get; set; }
    
    /// <summary>
    /// Optional content of the drawer
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    #endregion

    /// <summary>
    /// Determines if the drawer is open or closed
    /// </summary>
    private bool IsOpen { get; set; }

    /// <summary>
    /// Initialize Drawer component
    /// Subscribes to drawer events
    /// </summary>
    protected override void OnInitialized()
    {
        DrawerService.DrawerToggled += HandleDrawerToggled;
    }
    
    /// <summary>
    /// Dispose of Drawer component.
    /// Unsubscribes from drawer service.
    /// </summary>
    public void Dispose()
    {
        DrawerService.DrawerToggled -= HandleDrawerToggled;
    }
    
    /// <summary>
    /// Handles DrawerToggled events from the <see cref="IDrawerService"/>
    /// </summary>
    private void HandleDrawerToggled(object? sender, DrawerArgs e)
    {
        if (!e.Id.Equals(Id)) return;
        
        IsOpen = e.IsOpen;
        StateHasChanged(); // TODO seems sketchy!
    }
    
    /// <summary>
    /// Handle Close Drawer Clicks
    /// </summary>
    private void CloseDrawer() => DrawerService.CloseDrawer(Id);
}