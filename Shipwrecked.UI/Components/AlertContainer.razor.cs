using Microsoft.AspNetCore.Components;
using Shipwrecked.UI.Models;
using Shipwrecked.UI.Services;

namespace Shipwrecked.UI.Components;

public partial class AlertContainer : ComponentBase
{
    
    [Inject] private IAlertService AlertService { get; set; } = default!;


    private List<Alert> Alerts = new();
        
    protected override void OnInitialized()
    {
        Alerts = AlertService.GetAlerts();
        AlertService.OnChange += StateHasChanged;
    }
}