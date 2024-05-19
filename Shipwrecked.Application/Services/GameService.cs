using Ardalis.GuardClauses;
using Fluxor;
using Shipwrecked.Application.Actions;
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
    private IDispatcher Dispatcher { get; set; }
    private readonly IGameSettingsFactory _gameSettingsFactory;
    
    /// <summary>
    /// Constructor specifying all dependencies
    /// </summary>
    public GameService(IDispatcher dispatcher, IGameSettingsFactory gameSettingsFactory)
    {
        Dispatcher = Guard.Against.Null(dispatcher);
        _gameSettingsFactory = Guard.Against.Null(gameSettingsFactory);
    }

    /// <inheritdoc />
    public void StartNewGame(GameDifficulty difficulty)
    {
        Dispatcher.Dispatch(new LoadGameAction());
        
        var game = new Game
        {
            Id = Guid.NewGuid(),
            Day = 1,
            Difficulty = difficulty,
            Settings = _gameSettingsFactory.Create(difficulty)
        };

        Dispatcher.Dispatch(new GameLoadedAction(game));
    }
    
    /// <inheritdoc />
    public void IncrementDay()
    {
        Dispatcher.Dispatch(new IncrementDayAction());
    }
}