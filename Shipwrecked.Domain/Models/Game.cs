using Shipwrecked.Domain.Enums;

namespace Shipwrecked.Domain.Models;

/// <summary>
/// Represents a Game object
/// </summary>
public class Game
{
    /// <summary>
    /// A unique identifier for the game
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The current day in the game
    /// </summary>
    public int Day { get; set; }
    
    /// <summary>
    /// The difficulty level of the game.
    /// Used to set some game settings
    /// </summary>
    public GameDifficulty Difficulty { get; set; }

    /// <summary>
    /// The game settings
    /// </summary>
    public GameSettings Settings { get; set; } = new ();
}