using Ardalis.GuardClauses;
using Shipwrecked.Application.Factories;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure.Interfaces;

namespace Shipwrecked.Application.Services;

/// <summary>
/// Implementation of the <see cref="IPlayerService"/> interface
/// </summary>
public class PlayerService : IPlayerService
{
    // TODO what if the player obj just lived here?
    
    private readonly IPlayerStore _playerStore;
    
    /// <summary>
    /// Constructor specifying all dependencies
    /// </summary>
    public PlayerService(IPlayerStore playerStore)
    {
        _playerStore = Guard.Against.Null(playerStore);
    }
    
    /// <inheritdoc />
    public void CreatePlayer(string name, Gender gender, GameDifficulty difficulty)
    {
        Guard.Against.NullOrWhiteSpace(name);
        
        Player player = PlayerFactory.CreatePlayer(name, gender, difficulty);
        
        _playerStore.UpdatePlayer(player);
    }
}