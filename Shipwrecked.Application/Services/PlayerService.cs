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
    private readonly IContext _context;
    
    /// <summary>
    /// Constructor specifying all dependencies
    /// </summary>
    public PlayerService(IContext context)
    {
        _context = Guard.Against.Null(context);
    }
    
    /// <inheritdoc />
    public Player CreatePlayer(string name, Gender gender, GameDifficulty difficulty)
    {
        Guard.Against.NullOrWhiteSpace(name);
        
        Player player = PlayerFactory.CreatePlayer(name, gender, difficulty);
        
        _context.SetPlayerState(player);

        return player;
    }
}