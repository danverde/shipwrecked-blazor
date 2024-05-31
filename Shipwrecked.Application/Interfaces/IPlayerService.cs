using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Interfaces;

/// <summary>
/// Interface for interacting with the Player.
/// Returns associated effects
/// </summary>
public interface IPlayerService
{
    /// <summary>
    /// Create a player object
    /// </summary>
    public Player CreatePlayer(string name, Gender gender, GameDifficulty difficulty);
    
    /// <summary>
    /// Handles Changes to the player made when the day is incremented.
    /// Returns a list of actions as actions.
    /// </summary>
    /// <returns>A list of actions that need to be dispatched as a result of this method</returns>
    public List<object> IncrementDay(Player player);
}