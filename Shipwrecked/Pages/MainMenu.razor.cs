using Microsoft.AspNetCore.Components;
using Shipwrecked.Models;
using Shipwrecked.Services;

namespace Shipwrecked.Pages;

/// <summary>
/// Code behind Main Menu page
/// </summary>
public partial class MainMenu
{
    [Inject] private IDrawerService _DrawerService { get; set; }

    private void HelpMenuClicked()
    {
        Console.WriteLine("Open Called");
        _DrawerService.OpenDrawer(DrawerId.HelpMenu);
    } 
}