namespace Shipwrecked.Domain.Models;

public class Player : Character
{
    public int Stamina { get; set; }
    public int MaxStamina { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
}