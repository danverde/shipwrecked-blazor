using Ardalis.GuardClauses;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure.Interfaces;
using Shipwrecked.Infrastructure.Models;

namespace Shipwrecked.Application.Services;

/// <summary>
/// Implementation of the <see cref="IAppStateService"/> interface
/// </summary>
public class AppStateService : IAppStateService
{
    private readonly IAppStateStore _appStateStore;
    
    public AppStateService(IAppStateStore appStateStore)
    {
        _appStateStore = Guard.Against.Null(appStateStore);
    }
    
    /// <inheritdoc />
    public Task<bool> ExistsAsync(Guid id)
    {
        Guard.Against.NullOrEmpty(id);
        
        return _appStateStore.ExistsAsync(id);
    }

    /// <inheritdoc />
    public async Task<IList<AppState>> ListAsync()
    {
        return await _appStateStore.ListAppStatesAsync(); 
    }

    /// <inheritdoc />
    public async Task<AppState?> LoadAsync(Guid id)
    {
        Guard.Against.NullOrEmpty(id);

        bool exists = await ExistsAsync(id);
        if (!exists)
            return null;
        
        return await _appStateStore.LoadAsync(id);
    }

    /// <inheritdoc />
    public async Task<AppState> SaveAsync(Game game, Player player)
    {
        Guard.Against.Null(game);
        Guard.Against.Null(player);
        
        var appState = new AppState {Game = game, Player = player};
        
        await _appStateStore.SaveAsync(appState);
        return appState;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        Guard.Against.NullOrEmpty(id);

        await _appStateStore.DeleteAsync(id);
    }
}