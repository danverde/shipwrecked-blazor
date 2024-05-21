using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.Domain.Models;

[ExcludeFromCodeCoverage]
public class Character
{
    public Guid Id { get; set; }
    public string ProfileImgUrl { get; set; } = "";
    public string Name { get; set; } = "";
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    // public CharacterStatus Status { get; set; }
    public Inventory Inventory { get; set; } = default!;
    public Location Location { get; set; } = default!;
}