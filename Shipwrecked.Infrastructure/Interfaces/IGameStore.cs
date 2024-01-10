using Shipwrecked.Domain.Models;

namespace Shipwrecked.Infrastructure.Interfaces;

/// <summary>
/// Interface for persisting changes to a Game object
/// </summary>
public interface IGameStore
{
    /// <summary>
    /// Update a game object
    /// </summary>
    void UpdateGame(Game game);
}