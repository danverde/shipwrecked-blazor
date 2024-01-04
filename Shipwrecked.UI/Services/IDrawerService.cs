using Shipwrecked.UI.Models;

namespace Shipwrecked.UI.Services;

public interface IDrawerService
{
    // void RegisterDrawer(string key, bool isOpen = false);
    
    public event EventHandler<DrawerArgs> DrawerToggled;
    
    /// <summary>
    /// Open a drawer by its id
    /// </summary>
    void OpenDrawer(DrawerId id);
    
    /// <summary>
    /// Close a drawer by its id
    /// </summary>
    void CloseDrawer(DrawerId id);
    
    // /// <summary>
    // /// Toggle a drawer by its name
    // /// </summary>
    // void ToggleDrawer(string key);
    
}