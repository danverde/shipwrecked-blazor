using Ardalis.GuardClauses;
using Blazored.LocalStorage;
using Shipwrecked.Infrastructure;
using Shipwrecked.Infrastructure.Interfaces;

namespace Shipwrecked.UI.Services;

/// <summary>
/// Implementation of the <see cref="IStateStorage"/> interface
/// </summary>
public class LocalStorageStore : IStateStorage
{
    private readonly ILocalStorageService _localStorageService;
    
    /// <summary>
    /// Public constructor specifying all dependencies
    /// </summary>
    public LocalStorageStore(ILocalStorageService localStorageService)
    {
        _localStorageService = Guard.Against.Null(localStorageService);
    }
    
    /// <inheritdoc />
    public async Task<IList<State>> ListSavedStatesAsync()
    {
        List<string> keys = (await _localStorageService.KeysAsync()).ToList();

        List<State> states = new List<State>();
        foreach (var key in keys)
        {
            var state = await _localStorageService.GetItemAsync<State>(key);
            states.Add(state);
        }

        return states;
    }

    /// <inheritdoc />
    public async Task<State> LoadStateAsync(Guid id)
    {
        return await _localStorageService.GetItemAsync<State>(id.ToString());
    }

    /// <inheritdoc />
    public async Task SaveStateAsync(Guid gameId, State state)
    {
        Guard.Against.Null(state);
        
        await _localStorageService.SetItemAsync(gameId.ToString(), state);
    }

    /// <inheritdoc />
    public async Task DeleteStateAsync(Guid id)
    {
        await _localStorageService.RemoveItemAsync(id.ToString());
    }
    
    /// <inheritdoc />
    public async Task DeleteStatesAsync(IEnumerable<Guid> ids)
    {
        await _localStorageService.RemoveItemsAsync(ids.Select(id => id.ToString()));
    }
}