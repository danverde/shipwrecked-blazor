using Microsoft.AspNetCore.Components;
using Shipwrecked.UI.Interfaces;
using Shipwrecked.UI.Models;
using Shipwrecked.UI.Services;

namespace Shipwrecked.UI.Pages;

/// <summary>
/// Code behind Main Menu page
/// </summary>
public partial class MainMenu
{
    /// <summary>
    /// <see cref="IDrawerService"/> used to dispatch drawer events
    /// </summary>
    [Inject] private IDrawerService _DrawerService { get; set; }

    /// <summary>
    /// Handles click events for the Help Menu button
    /// </summary>
    private void HelpMenuClicked() => _DrawerService.OpenDrawer(DrawerId.HelpMenu);
}