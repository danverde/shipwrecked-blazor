using System.Collections.Concurrent;
using Shipwrecked.UI.Models;

namespace Shipwrecked.UI.Services;

/// <summary>
/// Service to manage the state of drawers throughout the UI
/// </summary>
public class DrawerService : IDrawerService
{
    private ConcurrentDictionary<string, bool> Drawers { get; set; } = new();

    public event EventHandler<DrawerArgs> DrawerToggled;
    
    public void OpenDrawer(DrawerId id)
    {
        // TODO update dictionary & pass through?
        
        OnDrawerToggled(new DrawerArgs(id, true));
    }
    
    public void CloseDrawer(DrawerId id)
    {
        // TODO update dictionary & pass through?
        
        OnDrawerToggled(new DrawerArgs(id, false));
    }
    
    private void OnDrawerToggled(DrawerArgs args)
    {
        // Make a temporary copy of the event to avoid possibility of
        // a race condition if the last subscriber unsubscribes
        // immediately after the null check and before the event is raised.
        EventHandler<DrawerArgs> raiseEvent = DrawerToggled;

        // Event will be null if there are no subscribers
        if (raiseEvent != null)
        {
            // Call to raise the event.
            raiseEvent(this, args);
        }
    }
    
}