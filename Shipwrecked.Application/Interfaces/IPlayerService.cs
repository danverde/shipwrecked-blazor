using Shipwrecked.Domain.Enums;

namespace Shipwrecked.Application.Interfaces;

/// <summary>
/// Interface for interacting with the Player
/// </summary>
public interface IPlayerService
{
    /// <summary>
    /// Create a new Player
    /// </summary>
    void CreatePlayer(string name, Gender gender, GameDifficulty difficulty);
}