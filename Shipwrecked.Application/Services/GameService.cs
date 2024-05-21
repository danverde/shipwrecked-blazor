using Ardalis.GuardClauses;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Services;

/// <summary>
/// Implementation of the <see cref="IGameService"/> interface
/// </summary>
public class GameService : IGameService
{
    private readonly IGameSettingsFactory _gameSettingsFactory;
    
    /// <summary>
    /// Constructor specifying all dependencies
    /// </summary>
    public GameService(IGameSettingsFactory gameSettingsFactory)
    {
        _gameSettingsFactory = Guard.Against.Null(gameSettingsFactory);
    }

    /// <inheritdoc />
    public Game StartNewGame(GameDifficulty difficulty)
    {
        return new Game
        {
            Id = Guid.NewGuid(),
            Day = 1,
            Difficulty = difficulty,
            Settings = _gameSettingsFactory.Create(difficulty)
        };
    }
}