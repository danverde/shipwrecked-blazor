using Ardalis.GuardClauses;
using Shipwrecked.Domain.Interfaces;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Domain.Managers;

/// <summary>
/// Implementation of the <see cref="IPlayerManager"/> interface
/// </summary>
public class PlayerManager : IPlayerManager
{
    /// <inheritdoc />
    public Player DecreaseStamina(Player player, Settings settings)
    {
        Guard.Against.Null(player);
        Guard.Against.Null(settings);

        player = Util.Clone(player);

        player.Stamina -= settings.StaminaPerDay;
        if (player.Stamina < 0)
            player.Stamina = 0;

        return player;
    }

    /// <inheritdoc />
    public Player IncreaseExp(Player player, Settings settings)
    {
        Guard.Against.Null(player);
        Guard.Against.Null(settings);

        player = Util.Clone(player);
        
        player.Experience += settings.ExpPerDay;
        if (player.Experience < 100) return player;
        
        player.Experience -= 100;
        player.Level++;
        player.MaxHealth += settings.HealthGrowth;
        player.Health += settings.HealthGrowth;
        player.Stamina += settings.StaminaGrowth;
        player.MaxStamina += settings.StaminaGrowth;

        return player;
    }
}