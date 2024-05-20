using Shipwrecked.Application.Actions;
using Shipwrecked.Infrastructure.Models;

namespace Shipwrecked.Application.Interfaces;

/// <summary>
/// Interface for persisting/loading application states
/// </summary>
public interface IAppStateService
{
    /// <summary>
    /// Determines if a state exists in storage
    /// </summary>
    Task<bool> SaveGameExistsAsync(Guid id);
    
    /// <summary>
    /// Get a list of all saved games.
    /// </summary>
    Task<IList<AppState>> ListSavedGamesAsync();

    /// <summary>
    /// Load a single game by its Id
    /// </summary>
    Task<AppState> LoadGameAsync(Guid id);
    
    /// <summary>
    /// Save the current state
    /// </summary>
    Task<AppState> SaveGameAsync(Guid gameId, SaveGameAction action);

    /// <summary>
    /// Delete a saved game by its id
    /// </summary>
    Task DeleteGameAsync(Guid id);
    
    // /// <summary>
    // /// Delete multiple games by id
    // /// </summary>
    // Task DeleteGamesAsync(IEnumerable<Guid> ids);
}