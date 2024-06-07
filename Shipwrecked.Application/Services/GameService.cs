using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Services;

/// <summary>
/// Implementation of the <see cref="IGameService"/> interface
/// </summary>
public class GameService : IGameService
{
    /// <inheritdoc />
    public Game CreateGame(GameDifficulty difficulty)
    {
        return new Game
        {
            Id = Guid.NewGuid(),
            Day = 1,
            Difficulty = difficulty,
        };
    }
}