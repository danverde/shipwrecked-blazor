using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using Blazored.LocalStorage;
using Shipwrecked.Infrastructure.Interfaces;
using Shipwrecked.Infrastructure.Models;

namespace Shipwrecked.Infrastructure.Services;

/// <summary>
/// Implementation of the <see cref="IAppStateStore"/> interface
/// </summary>
public class AppStateStore : IAppStateStore
{
    private const string StatePrefix = "sg:";
    
    private readonly ILocalStorageService _localStorageService;
    
    /// <summary>
    /// Public constructor specifying all dependencies
    /// </summary>
    public AppStateStore(ILocalStorageService localStorageService)
    {
        _localStorageService = Guard.Against.Null(localStorageService);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(Guid id)
    {
        var key = $"{StatePrefix}{id}";
        return await _localStorageService.ContainKeyAsync(key);
    }
    
    /// <inheritdoc />
    public async Task<IList<AppState>> ListAppStatesAsync()
    {
        var r = new Regex("^sg:");
        
        List<string> keys = (await _localStorageService.KeysAsync()).ToList();
        keys = keys.Where(k => r.IsMatch(k)).ToList();
        
        List<AppState> states = new List<AppState>();
        foreach (var key in keys)
        {
            var state = await _localStorageService.GetItemAsync<AppState>(key);
            states.Add(state);
        }

        return states;
    }

    /// <inheritdoc />
    public async Task<AppState> LoadAsync(Guid id)
    {
        var key = $"{StatePrefix}{id}";
        AppState appState = await _localStorageService.GetItemAsync<AppState>(key);
        
        return appState;
    }

    /// <inheritdoc />
    public async Task SaveAsync(Guid gameId, AppState appState)
    {
        Guard.Against.Null(appState);

        var key = $"{StatePrefix}{gameId}";
        await _localStorageService.SetItemAsync(key, appState);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        var key = $"{StatePrefix}{id}";
        await _localStorageService.RemoveItemAsync(key);
    }
    
    // /// <inheritdoc />
    // public async Task DeleteGamesAsync(IEnumerable<Guid> ids)
    // {
    //     Guard.Against.Null(ids);
    //     
    //     IEnumerable<string> keys = ids.ToList().Select(id => $"{StatePrefix}{id}");
    //     
    //     await _localStorageService.RemoveItemsAsync(keys);
    // }
}