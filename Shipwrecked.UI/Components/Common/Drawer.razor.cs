using Microsoft.AspNetCore.Components;

namespace Shipwrecked.UI.Components.Common;

/// <summary>
/// Code behind for the Drawer Component
/// </summary>
public partial class Drawer
{
    [Parameter] public string? Title { get; set; }
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public EventCallback<bool> IsOpenChanged { get; set; }
    
    async Task UpdateIsOpen()
    {
        await IsOpenChanged.InvokeAsync(IsOpen);
    }

    private async Task CloseDrawer()
    {
        IsOpen = false;
        await UpdateIsOpen();
    }
    
}