using Shipwrecked.Infrastructure.Models;

namespace Shipwrecked.Infrastructure.Interfaces;

/// <summary>
/// Interface defining functionality to manage application states.
/// </summary>
public interface IAppStateStore
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
    Task SaveGameAsync(Guid gameId, AppState appState);

    /// <summary>
    /// Delete a saved game by its id
    /// </summary>
    Task DeleteGameAsync(Guid id);
    
    // /// <summary>
    // /// Delete multiple games by id
    // /// </summary>
    // Task DeleteGamesAsync(IEnumerable<Guid> ids);
}