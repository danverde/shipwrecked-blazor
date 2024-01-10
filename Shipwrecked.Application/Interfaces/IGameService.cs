using Shipwrecked.Domain.Enums;

namespace Shipwrecked.Application.Interfaces;

/// <summary>
/// Interface for interacting with the Game
/// </summary>
public interface IGameService
{
    /// <summary>
    /// Start a new Game with a given difficulty level
    /// </summary>
    void StartGame(GameDifficulty difficulty);
}