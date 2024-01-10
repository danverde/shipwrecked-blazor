using Ardalis.GuardClauses;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure.Interfaces;

namespace Shipwrecked.Infrastructure.Services;

/// <summary>
/// Implementation of the <see cref="IPlayerStore"/> interface
/// </summary>
public class PlayerStore : IPlayerStore
{
    /// <inheritdoc />
    public void UpdatePlayer(Player player)
    {
        Guard.Against.Null(player);
        
        State.Player = player;
    }
}