namespace Shipwrecked.Application.Actions;

public class SetExpAction
{
    public int Experience { get; set; } = default!;

    public SetExpAction(int exp)
    {
        Experience = exp;
    }
}