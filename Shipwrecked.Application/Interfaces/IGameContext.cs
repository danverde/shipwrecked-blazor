using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Interfaces;

public interface IGameContext
{
    /// <summary>
    /// Get the current Game
    /// </summary>
    Game GetGame();
    
    /// <summary>
    /// Start a new Game with a given difficulty level
    /// </summary>
    /// <returns>The newly created Game</returns>
    Game StartGame(GameDifficulty difficulty);
    
    /// <summary>
    /// Increment the day in the game
    /// </summary>
    /// <returns>The current day</returns>
    int IncrementDay();
}