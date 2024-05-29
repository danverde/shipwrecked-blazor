using Ardalis.GuardClauses;
using Shipwrecked.Domain.Constants;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Interfaces;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Domain.Managers;

/// <summary>
/// Implementation of the <see cref="IPlayerManager"/> interface
/// </summary>
public class PlayerManager : IPlayerManager
{
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
}