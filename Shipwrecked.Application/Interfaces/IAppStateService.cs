using Shipwrecked.Domain.Models;
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
    Task<bool> ExistsAsync(Guid id);
    
    /// <summary>
    /// Get a list of all saved games.
    /// </summary>
    Task<IList<AppState>> ListAsync();

    /// <summary>
    /// Load a single game by its Id
    /// </summary>
    Task<AppState?> LoadAsync(Guid id);
    
    /// <summary>
    /// Save the current state
    /// </summary>
    Task<AppState> SaveAsync(Game game, Player player);

    /// <summary>
    /// Delete a saved game by its id
    /// </summary>
    Task DeleteAsync(Guid id);
}