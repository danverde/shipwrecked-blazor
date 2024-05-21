using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.UI.Store.Game.Actions;

/// <summary>
/// Action triggered when the day ends
/// </summary>
[ExcludeFromCodeCoverage]
public class IncrementDayAction(int day)
{
    public int Day { get; set; } = day;
}