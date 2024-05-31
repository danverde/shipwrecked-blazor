using System.Diagnostics.CodeAnalysis;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Actions;

/// <summary>
/// Action dispatched when a player levels up 
/// </summary>
[ExcludeFromCodeCoverage]
public class LevelUpAction
{
    public int Level { get; set; } = default!;
    public int Experience { get; set; } = default!;
    public int Health { get; set; } = default!;
    public int MaxHealth { get; set; } = default!;
    public int Stamina { get; set; } = default!;
    public int MaxStamina { get; set; } = default!;

    public LevelUpAction(Player player)
    {
        Level = player.Level;
        Experience = player.Experience;
        Health = player.Health;
        MaxHealth = player.MaxHealth;
        Stamina = player.MaxStamina;
        MaxStamina = player.MaxStamina;
    }
    
    public LevelUpAction(int level, int exp, int health, int maxHealth, int stamina, int maxStamina)
    {
        Level = level;
        Experience = exp;
        Health = health;
        MaxHealth = maxHealth;
        Stamina = stamina;
        MaxStamina = maxStamina;
    }
}