
using Microsoft.AspNetCore.Components;

namespace Shipwrecked.UI.Components.Common;

public partial class Modal
{
    [Parameter] public string Title { get; set; } = "";
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public bool IsCloseable { get; set; } = true;
    
    private void CloseModal()
    {
        IsOpen = false;
    }
}