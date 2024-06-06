using Microsoft.AspNetCore.Components;
using Shipwrecked.UI.Services;

namespace Shipwrecked.UI.Pages;

/// <summary>
/// Code behind Main Menu page
/// </summary>
public partial class MainMenuPage
{
    [Inject] private AlertService AlertService { get; set; } = default!;
    
    private bool HelpDrawerOpen { get; set; }

    private void OpenDrawer()
    {
        HelpDrawerOpen = true;
    }
}