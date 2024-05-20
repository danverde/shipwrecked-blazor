using Ardalis.GuardClauses;
using Fluxor;
using Shipwrecked.Application.Actions;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

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
        Dispatcher.Dispatch(new StartGameAction());
        
        var game = new Game
        {
            Id = Guid.NewGuid(),
            Day = 1,
            Difficulty = difficulty,
            Settings = _gameSettingsFactory.Create(difficulty)
        };

        Dispatcher.Dispatch(new GameLoadedAction(game));
        // TODO dispatch additional actions to set the player, map, inventory, etc.
        // TODO hard to unit test this since it's passing the data straight to the dispatcher...
    }
    
    /// <inheritdoc />
    public void IncrementDay(Game game)
    {
        Guard.Against.Null(game);
        var newDay = game.Day + 1;
        
        Dispatcher.Dispatch(new IncrementDayAction(newDay));
    }
}