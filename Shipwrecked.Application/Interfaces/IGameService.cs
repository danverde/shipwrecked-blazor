using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Interfaces;

/// <summary>
/// Interface for interacting with the Game
/// </summary>
public interface IGameService
{
    /// <summary>
    /// Start a new Game with a given difficulty level
    /// </summary>
    Game CreateGame(GameDifficulty difficulty);
}
