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
    
    public Task<bool> SaveGameExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<AppState>> ListSavedGamesAsync()
    {
        return await _appStateStore.ListSavedGamesAsync(); 
    }

    public async Task<AppState> LoadGameAsync(Guid id)
    {
        Guard.Against.Null(id);

        return await _appStateStore.LoadGameAsync(id);
    }

    /// <inheritdoc />
    public async Task<AppState> SaveGameAsync(Guid gameId, SaveGameAction saveGameAction)
    {
        Guard.Against.Null(gameId);
        Guard.Against.Null(saveGameAction);
        
        // TODO use auto-mapper
        var appState = new AppState {Game = saveGameAction.Game};
        
        await _appStateStore.SaveGameAsync(saveGameAction.Game.Id, appState);
        return appState;
    }

    public Task DeleteGameAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}