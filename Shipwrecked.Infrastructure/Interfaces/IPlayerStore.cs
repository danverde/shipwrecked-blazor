using Shipwrecked.Domain.Models;

namespace Shipwrecked.Infrastructure.Interfaces;

/// <summary>
/// Interface for persisting changes to a Player object
/// </summary>
public interface IPlayerStore
{
    /// <summary>
    /// Update the player
    /// </summary>
    void UpdatePlayer(Player player);
}