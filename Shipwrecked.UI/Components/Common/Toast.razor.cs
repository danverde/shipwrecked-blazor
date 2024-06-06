using Microsoft.AspNetCore.Components;
using Shipwrecked.UI.Models;
using Shipwrecked.UI.Services;

namespace Shipwrecked.UI.Components.Common;

public partial class Toast : ComponentBase
{
    [Inject] private AlertService AlertService { get; set; } = default!;

    [Parameter] public Alert Alert { get; set; } = default!;

    private void DeleteToast()
    {
        AlertService.Delete(Alert);
    }
}