using Ardalis.GuardClauses;
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
    private readonly IGameSettingsFactory _gameSettingsFactory;
    private readonly IGameStore _gameStore;
    private Game _game;
    
    /// <summary>
    /// Constructor specifying all dependencies
    /// </summary>
    public GameService(IGameSettingsFactory gameSettingsFactory, IGameStore gameStore)
    {
        _gameSettingsFactory = Guard.Against.Null(gameSettingsFactory);
        _gameStore = Guard.Against.Null(gameStore);
        _game = new Game();
    }

    /// <inheritdoc />
    public void StartGame(GameDifficulty difficulty)
    {
        _game = new Game
        {
            Id = Guid.NewGuid(),
            Day = 1,
            Difficulty = difficulty,
            Settings = _gameSettingsFactory.Create(difficulty)
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