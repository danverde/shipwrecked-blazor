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
    private readonly IContext _context;
    
    /// <summary>
    /// Constructor specifying all dependencies
    /// </summary>
    public GameService(IGameSettingsFactory gameSettingsFactory, IContext context)
    {
        _gameSettingsFactory = Guard.Against.Null(gameSettingsFactory);
        _context = Guard.Against.Null(context);
    }

    /// <inheritdoc />
    public Game StartGame(GameDifficulty difficulty)
    {
        var game = new Game
        {
            Id = Guid.NewGuid(),
            Day = 1,
            Difficulty = difficulty,
            Settings = _gameSettingsFactory.Create(difficulty)
        };
        
        _context.SetGameState(game);

        return game;
    }
    
    /// <inheritdoc />
    public void IncrementDay()
    {
        Game game = _context.GetState().Game;
        
        game.Day += 1;
        
        // TODO pretty sure I don't have to update the store at this point...
        _context.SetGameState(game);
    }

}