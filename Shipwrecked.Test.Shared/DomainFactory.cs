using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shared;

/// <summary>
/// Helper class used to generate domain objects for unit tests
/// </summary>
public class DomainFactory
{
    
    /// <summary>
    /// Generate a Game object
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
}