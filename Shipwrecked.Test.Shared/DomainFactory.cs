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
    public static Game CreateGame()
    {
        return new Game()
        {
            Id = Guid.NewGuid(),
            Difficulty = GameDifficulty.Normal,
            Settings = new GameSettings()
        };
    }
}