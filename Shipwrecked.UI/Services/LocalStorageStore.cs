using System.Text.RegularExpressions;
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
    private const string StatePrefix = "sg:";
    
    private readonly ILocalStorageService _localStorageService;
    private readonly IContext _context;
    
    /// <summary>
    /// Public constructor specifying all dependencies
    /// </summary>
    public LocalStorageStore(ILocalStorageService localStorageService, IContext context)
    {
        _localStorageService = Guard.Against.Null(localStorageService);
        _context = Guard.Against.Null(context);
    }

    /// <inheritdoc />
    public async Task<bool> StateExistsAsync(Guid id)
    {
        var key = $"{StatePrefix}{id}";
        return await _localStorageService.ContainKeyAsync(key);
    }
    
    /// <inheritdoc />
    public async Task<IList<State>> ListSavedStatesAsync()
    {
        var r = new Regex("^sg:");
        
        List<string> keys = (await _localStorageService.KeysAsync()).ToList();
        keys = keys.Where(k => r.IsMatch(k)).ToList();
        
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
        var key = $"{StatePrefix}{id}";
        State state = await _localStorageService.GetItemAsync<State>(key);
        _context.SetState(state);
        
        return state;
    }

    /// <inheritdoc />
    public async Task SaveStateAsync(Guid gameId, State state)
    {
        Guard.Against.Null(state);

        var key = $"{StatePrefix}{gameId}";
        await _localStorageService.SetItemAsync(key, state);
    }

    /// <inheritdoc />
    public async Task DeleteStateAsync(Guid id)
    {
        var key = $"{StatePrefix}{id}";
        await _localStorageService.RemoveItemAsync(key);
    }
    
    /// <inheritdoc />
    public async Task DeleteStatesAsync(IEnumerable<Guid> ids)
    {
        Guard.Against.Null(ids);
        
        IEnumerable<string> keys = ids.ToList().Select(id => $"{StatePrefix}{id}");
        
        await _localStorageService.RemoveItemsAsync(keys);
    }
}