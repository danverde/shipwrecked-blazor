using System.Data;
using Ardalis.GuardClauses;
using Shipwrecked.Application.Factories;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure.Interfaces;

namespace Shipwrecked.Application.Services;

/// <summary>
/// Implementation of the <see cref="IGameService"/> interface
/// </summary>
public class GameService : IGameService
{
    private readonly IGameStore _gameStore;
    private Game _game;
    
    /// <summary>
    /// Constructor specifying all dependencies
    /// </summary>
    public GameService(IGameStore gameStore)
    {
        _gameStore = Guard.Against.Null(gameStore);
    }

    /// <inheritdoc />
    public void StartGame(GameDifficulty difficulty)
    {
        _game = new Game
        {
            Id = Guid.NewGuid(),
            Day = 1,
            GameSettings = GameSettingsFactory.Create(difficulty)
        };
        
        Update();
    }
    
    /// <inheritdoc />
    public void IncrementDay()
    {
        _game.Day += 1;
        
        Update();
    }

    #region Private

    /// <summary>
    /// Update the game store
    /// </summary>
    private void Update()
    {
        _gameStore.UpdateGame(_game);
    }
    
    #endregion
}