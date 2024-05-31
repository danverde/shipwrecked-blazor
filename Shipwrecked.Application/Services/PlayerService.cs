using Ardalis.GuardClauses;
using Shipwrecked.Application.Actions;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Constants;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Interfaces;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Services;

/// <summary>
/// Implementation of the <see cref="IPlayerService"/> interface
/// </summary>
public class PlayerService(IPlayerManager playerManager) : IPlayerService
{
    private readonly IPlayerManager _playerManager = Guard.Against.Null(playerManager);

    /// <inheritdoc />
    public Player CreatePlayer(string name, Gender gender, GameDifficulty difficulty)
    {
        Guard.Against.NullOrWhiteSpace(name);
        
        var player = new Player
        {
            Id = Guid.NewGuid(),
            Name = name,
            Level = 1,
            Experience = 0,
            Stamina = 15,
            MaxStamina = 20,
            Health = 20,
            MaxHealth = 20,
            ProfileImg = gender == Gender.Female ? ImgConstants.WomanProfileImg : ImgConstants.ManProfileImg,
            SpriteImg = gender == Gender.Female ? ImgConstants.WomanSprite : ImgConstants.ManSprite,
            Inventory = new Inventory(),
        };

        if (difficulty == GameDifficulty.Easy)
        {
            player.Stamina += 5;
        } else if (difficulty == GameDifficulty.Difficult)
        {
            player.Health -= 5;
        }

        return player;
    }

    /// <inheritdoc />
    public List<object> IncrementDay(Player player)
    {
        Guard.Against.Null(player);

        var effects = new List<object>();
        
        // Decrease stamina
        player = _playerManager.DecreaseStamina(player);
        effects.Add(new SetStaminaAction(player.Stamina));            
        if (player.Stamina == 0)
        {
            effects.Add(new GameOverAction());
            return effects;
        }
        
        var originalLevel = player.Level;
        player = _playerManager.IncreaseExp(player);
        if (player.Level > originalLevel)
        {
            effects.Add(new LevelUpAction(player));
        }
        else
        {
            effects.Add(new SetExpAction(player.Experience));
        }

        return effects;
    }
}