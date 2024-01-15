using Shipwrecked.Domain.Models;

namespace Shipwrecked.Infrastructure.Interfaces;

/// <summary>
/// Interface defining functionality to manage saved games
/// </summary>
public interface IStateStorage
{
    /// <summary>
    /// Get a list of all saved games.
    /// </summary>
    Task<IList<State>> ListSavedStatesAsync();

    /// <summary>
    /// Load a single game by it's Id
    /// </summary>
    Task<State> LoadStateAsync(Guid id);
    
    /// <summary>
    /// Save the current state
    /// </summary>
    Task SaveStateAsync(Guid gameId, State state);

    /// <summary>
    /// Delete a saved game by its id
    /// </summary>
    Task DeleteStateAsync(Guid id);
    
    /// <summary>
    /// Delete multiple games by id
    /// </summary>
    Task DeleteStatesAsync(IEnumerable<Guid> ids);
}