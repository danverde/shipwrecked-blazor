using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Domain.Interfaces;

/// <summary>
/// Interface for the service that interacts with Player objects
/// </summary>
public interface IPlayerManager
{
    /// <summary>
    /// Create a new Player
    /// </summary>
    Player CreatePlayer(string name, Gender gender, GameDifficulty difficulty);
}