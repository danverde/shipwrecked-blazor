using Ardalis.GuardClauses;
using Shipwrecked.Application.Actions;
using Shipwrecked.Application.Interfaces;
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
    public async Task<AppState> SaveAsync(Guid id, SaveGameAction saveGameAction)
    {
        Guard.Against.NullOrEmpty(id);
        Guard.Against.Null(saveGameAction);
        
        // TODO use auto-mapper
        var appState = new AppState {Game = saveGameAction.Game};
        
        await _appStateStore.SaveAsync(saveGameAction.Game.Id, appState);
        return appState;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        Guard.Against.NullOrEmpty(id);

        await _appStateStore.DeleteAsync(id);
    }
}