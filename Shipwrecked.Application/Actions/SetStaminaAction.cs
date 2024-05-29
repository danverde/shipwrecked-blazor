namespace Shipwrecked.Application.Actions;

public class SetStaminaAction
{
    public int Stamina { get; set; } = default!;

    public SetStaminaAction(int stamina)
    {
        Stamina = stamina;
    }
}