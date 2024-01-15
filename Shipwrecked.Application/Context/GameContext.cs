using Ardalis.GuardClauses;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Context;

/// <summary>
/// Implementation of the <see cref="IGameContext"/> interface
/// </summary>
public class GameContext : IGameContext
{
    private readonly IGameSettingsFactory _gameSettingsFactory;
    private Game _game;
    
    /// <summary>
    /// Constructor specifying all dependencies
    /// </summary>
    public GameContext(IGameSettingsFactory gameSettingsFactory)
    {
        _gameSettingsFactory = Guard.Against.Null(gameSettingsFactory);
        _game = new Game();
    }
    
    /// <inheritdoc />
    public Game GetGame() => _game;
    
    /// <inheritdoc />
    public Game StartGame(GameDifficulty difficulty)
    {
        _game = new Game
        {
            Id = Guid.NewGuid(),
            Day = 1,
            Difficulty = difficulty,
            Settings = _gameSettingsFactory.Create(difficulty)
        };

        return _game;
    }
    
    /// <inheritdoc />
    public int IncrementDay()
    {
        _game.Day += 1;
        
        return _game.Day;
    }

}