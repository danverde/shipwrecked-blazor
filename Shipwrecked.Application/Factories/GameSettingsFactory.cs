using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Factories;

/// <summary>
/// Factory used to generate GameSettings objects
/// </summary>
public class GameSettingsFactory : IGameSettingsFactory
{
    /// <inheritdoc />
    public GameSettings Create(GameDifficulty difficulty)
    {
        var waitSuccessRate = 100000;
        var staminaPerDay = 0;
        
        switch (difficulty)
        {
            case GameDifficulty.Easy:
                staminaPerDay = 3;
                break;
            case GameDifficulty.Normal:
                staminaPerDay = 4;
                break;
            case GameDifficulty.Difficult:
                staminaPerDay = 5;
                break;
        }

        return new GameSettings
        {
            WaitSuccessRate = waitSuccessRate,
            StaminaPerDay = staminaPerDay
        };
    }
}