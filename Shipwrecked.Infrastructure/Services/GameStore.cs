using Ardalis.GuardClauses;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure.Interfaces;

namespace Shipwrecked.Infrastructure.Services;

/// <summary>
/// Implementation of the <see cref="IGameStore"/> interface
/// </summary>
public class GameStore : IGameStore
{
    /// <inheritdoc />
    public void UpdateGame(Game game)
    {
        Guard.Against.Null(game);
        
        State.Game = game;
    }
}