using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.Application.Actions;

/// <summary>
/// Action used to set the players experience stat
/// </summary>
[ExcludeFromCodeCoverage]
public class SetExpAction
{
    public int Experience { get; set; } = default!;

    public SetExpAction(int exp)
    {
        Experience = exp;
    }
}