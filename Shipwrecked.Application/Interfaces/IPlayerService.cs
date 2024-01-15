using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Interfaces;

/// <summary>
/// Interface for interacting with the Player
/// </summary>
public interface IPlayerService
{
    /// <summary>
    /// Create a new Player
    /// </summary>
    Player CreatePlayer(string name, Gender gender, GameDifficulty difficulty);
}