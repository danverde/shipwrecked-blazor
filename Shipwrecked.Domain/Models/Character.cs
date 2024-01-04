namespace Shipwrecked.Domain.Models;

public class Character
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public int BaseAttack { get; set; }
    public int BaseDefense { get; set; }
    // public CharacterStatus Status { get; set; }
    public Inventory Inventory { get; set; }
    public int Col { get; set; }
    public int Row { get; set; }
}