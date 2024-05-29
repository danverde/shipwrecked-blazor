using Ardalis.GuardClauses;
using Shipwrecked.Application.Actions;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Interfaces;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Services;

/// <summary>
/// Implementation of the <see cref="IPlayerService"/> interface
/// </summary>
public class PlayerService : IPlayerService
{
    private readonly IPlayerManager _playerManager;

    public PlayerService(IPlayerManager playerManager)
    {
        _playerManager = Guard.Against.Null(playerManager);
    }


    /// <inheritdoc />
    public Player CreatePlayer(string name, Gender gender, GameDifficulty difficulty)
    {
        return _playerManager.CreatePlayer(name, gender, difficulty);
    }

    /// <inheritdoc />
    public List<object> IncrementDay(Player player)
    {
        // TODO add some sort of appsettings/gamesettings service for stat growth numbers
        
        /*
         * TODO use a manager to actually make the changes to the player? is it really worth it?
         * Can I really separate the two?
         */
        
        
        var effects = new List<object>();
        
        // Decrease stamina
        player.Stamina -= 3;
        if (player.Stamina < 0)
        {
            player.Stamina = 0;
            effects.Add(new SetStaminaAction(player.Stamina));
            effects.Add(new GameOverAction());
            return effects;
        }
        else
        {
            effects.Add(new SetStaminaAction(player.Stamina));
        }
        
        // gain exp
        player.Experience += 25;
        if (player.Experience >= 100)
        {
            player.Experience -= 100;
            player.Level++;
            player.MaxHealth += 3;
            player.Health += 3;
            effects.Add(new LevelUpAction(player));
        }
        
        effects.Add(new SetExpAction(player.Experience));

        return effects;
    }
}