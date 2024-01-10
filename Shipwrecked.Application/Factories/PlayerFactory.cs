using Ardalis.GuardClauses;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Factories;

/// <summary>
/// Factory used to create player objects
/// </summary>
public static class PlayerFactory
{
    /// <summary>
    /// Create a new player with the corresponding name
    /// & stats based off the game difficulty
    /// </summary>
    /// D<returns></returns>
    public static Player CreatePlayer(string name, Gender gender, GameDifficulty difficulty)
    {
        Guard.Against.NullOrWhiteSpace(name);

        var stamina  = 0;
        var maxStamina = 0;
        var health = 0;
        var maxHealth = 0;
        
        var player = new Player
        {
            Id = Guid.NewGuid(),
            Name = name,
            Level = 1,
            Experience = 0,
            ProfileImgUrl = gender == Gender.Female ? "/img/sprites/woman/woman.gif" : "/img/sprites/man/man.gif",
            Inventory = new Inventory(),
        };

        switch (difficulty)
        {
            case GameDifficulty.Easy:
                stamina = 20;
                maxStamina = 20;
                health = 20;
                maxHealth = 20;
                break;
            case GameDifficulty.Normal:
                stamina = 15;
                maxStamina = 20;
                health = 20;
                maxHealth = 20;
                break;
            case GameDifficulty.Difficult:
                stamina = 15;
                maxStamina = 20;
                health = 15;
                maxHealth = 20;
                break;
        }

        player.Stamina = stamina;
        player.MaxStamina = maxStamina;
        player.Health = health;
        player.MaxHealth = maxHealth;
        
        return player;
    }
}