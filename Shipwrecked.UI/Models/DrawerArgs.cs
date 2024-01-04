namespace Shipwrecked.UI.Models;

/// <summary>
/// Represents the payload of a DrawerToggled Event
/// </summary>
public class DrawerArgs : EventArgs
{
    /// <summary>
    /// Constructor specifying all dependencies
    /// </summary>
    public DrawerArgs(DrawerId drawerId, bool isOpen)
    {
        Id = drawerId;
        IsOpen = isOpen;
    }

    /// <summary>
    /// The unique Id of the drawer
    /// </summary>
    public DrawerId Id { get; set; }
    
    /// <summary>
    /// Determines if the Drawer is open or closed
    /// </summary>
    public bool IsOpen { get; set; }
}