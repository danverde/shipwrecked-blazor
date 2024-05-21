using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shared;

/// <summary>
/// Helper class used to generate domain objects for unit tests
/// </summary>
public class DomainFactory
{
    
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
            Settings = new GameSettings()
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
            MaxHealth = 10,
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