using Microsoft.AspNetCore.Components;
using Shipwrecked.UI.Services;

namespace Shipwrecked.UI.Components;

public partial class AlertContainer : ComponentBase
{
    [Inject] private AlertService AlertService { get; set; } = default!;
    
    protected override void OnInitialized()
    {
        AlertService.OnChange += StateHasChanged;
    }
}