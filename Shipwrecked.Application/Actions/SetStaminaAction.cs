using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.Application.Actions;

/// <summary>
/// Action used to set the players stamina stat
/// </summary>
[ExcludeFromCodeCoverage]
public class SetStaminaAction
{
    public int Stamina { get; set; } = default!;

    public SetStaminaAction(int stamina)
    {
        Stamina = stamina;
    }
}