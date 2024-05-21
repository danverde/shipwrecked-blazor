using Ardalis.GuardClauses;
using Shipwrecked.Application.Factories;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Services;

/// <summary>
/// Implementation of the <see cref="IPlayerService"/> interface
/// </summary>
public class PlayerService : IPlayerService
{
    private const string MaleUrl = "/img/sprites/man/man.gif";
    private const string FemaleUrl = "/img/sprites/woman/woman.gif";
    
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
            ProfileImgUrl = gender == Gender.Female ? FemaleUrl : MaleUrl,
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