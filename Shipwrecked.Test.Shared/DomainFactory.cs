using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shared;

/// <summary>
/// Helper class used to generate domain objects for unit tests
/// </summary>
public static class DomainFactory
{

    /// <summary>
    /// Create a basic <see cref="Settings"/> object for testing
    /// </summary>
    /// <returns></returns>
    public static Settings CreateSettings()
    {
        return new Settings
        {
            WaitSuccessRate = 10000,
            ExpPerDay = 25,
            StaminaPerDay = 3,
            InitialStamina = 15,
            MaxStamina = 20,
            InitialHealth = 20,
            MaxHealth = 20,
            StaminaGrowth = 1,
            HealthGrowth = 1
        };
    } 
    
    /// <summary>
    /// Generate a Game object for unit testing
    /// </summary>
    public static Game CreateGame(Guid? id = null)
    {
        var gameId = id ?? Guid.NewGuid(); 
        
        return new Game
        {
            Id = gameId,
            Difficulty = GameDifficulty.Normal,
        };
    }

    /// <summary>
    /// Generate a player object for unit testing
    /// </summary>
    public static Player CreatePlayer()
    {
        return new Player
        {
            Id = Guid.NewGuid(),
            Stamina = 10,
            MaxStamina = 20,
            Health = 10,
            MaxHealth = 20,
            Level = 1,
            Experience = 0,
            Name = "generic player",
            Location = new Location
            {
                X = 1,
                Y = 0
            },
            Inventory = new Inventory()
        };
    }
}