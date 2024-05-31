using Microsoft.AspNetCore.Components;
using Shipwrecked.UI.Models;

namespace Shipwrecked.UI.Components.Common;

internal enum ProgressType {
    Default,
    Warning,
    Critical
}

public partial class Gauge : ComponentBase
{
    private const int WarningPercent = 40;
    private const int CriticalPercent = 20;
    
    [Parameter] public string Label { get; set; } = default!;
    [Parameter] public int Max { get; set; }
    [Parameter] public int Value { get; set; }
    [Parameter] public GaugeType Type { get; set; } = GaugeType.Default;

    private string _gaugePercentString = "0%";
    private ProgressType _progressType = ProgressType.Default;

    protected override void OnParametersSet()
    {
        SetWidth();
        base.OnParametersSet();
    }

    private void SetWidth() {
        float gaugePercent = (float)Value / Max * 100;
        _gaugePercentString = $"{gaugePercent}%";

        if (Type == GaugeType.Default) {
            SetProgressType(gaugePercent);
        }
    }

    private string GetProgressTypeString()
    {
        var progressType = "default";
        switch (_progressType)
        {
            case ProgressType.Critical:
                progressType = "critical";
                break;
            case ProgressType.Warning:
                progressType = "warning";
                break;
        }

        return progressType;
    }
    
    private void SetProgressType(float gaugePercent) {
        if (gaugePercent > WarningPercent) {
            _progressType = ProgressType.Default;
        } else if (gaugePercent > CriticalPercent)
        {
            _progressType = ProgressType.Warning;
        } else {
            _progressType = ProgressType.Critical;
        }
    }
}