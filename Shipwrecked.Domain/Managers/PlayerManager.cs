using Ardalis.GuardClauses;
using Shipwrecked.Domain.Interfaces;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Domain.Managers;

/// <summary>
/// Implementation of the <see cref="IPlayerManager"/> interface
/// </summary>
/// TODO changes SHOULD be based off difficulty setting!
public class PlayerManager : IPlayerManager
{
    /// <inheritdoc />
    public Player DecreaseStamina(Player player)
    {
        Guard.Against.Null(player);

        player = Util.Clone(player);
        
        player.Stamina -= 3;
        if (player.Stamina < 0)
            player.Stamina = 0;

        return player;
    }

    /// <inheritdoc />
    public Player IncreaseExp(Player player)
    {
        Guard.Against.Null(player);

        player = Util.Clone(player);
        
        player.Experience += 25;
        if (player.Experience < 100) return player;
        
        player.Experience -= 100;
        player.Level++;
        player.MaxHealth += 3;
        player.Health += 3;

        return player;
    }
}