using Microsoft.AspNetCore.Components;
using Shipwrecked.Models;
using Shipwrecked.Services;

namespace Shipwrecked.Components;

/// <summary>
/// Code behind for the Drawer Component
/// </summary>
public partial class Drawer : IDisposable
{
    [Inject] private IDrawerService DrawerService { get; set; }
    
    [Parameter] public DrawerId Id { get; set; }
    
    [Parameter] public string? Title { get; set; }
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    private bool IsOpen { get; set; }

    protected override void OnInitialized()
    {
        DrawerService.DrawerToggled += HandleDrawerToggled;
    }
    
    public void Dispose()
    {
        DrawerService.DrawerToggled -= HandleDrawerToggled;
    }
    
    private void HandleDrawerToggled(object? sender, DrawerArgs e)
    {
        Console.WriteLine("Event Received by Drawer Component!");
        Console.WriteLine($"IsOpen: {e.IsOpen}");
        if (e.Id.Equals(Id))
        {
            IsOpen = e.IsOpen;
            StateHasChanged(); // TODO seems sketchy!
        }
    }
    
    private void CloseDrawer() => DrawerService.CloseDrawer(Id);
}