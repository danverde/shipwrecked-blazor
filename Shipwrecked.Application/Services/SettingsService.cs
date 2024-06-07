using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Services;

/// <summary>
/// Factory used to generate GameSettings objects
/// </summary>
public class SettingsService : ISettingsService
{
    /// <inheritdoc />
    public Settings GetSettings(GameDifficulty difficulty)
    {
        // defaults are used for normal games
        var waitSuccessRate = 10000;
        var expPerDay = 25;
        var staminaPerDay = 3;
        var initialStamina = 15;
        var maxStamina = 20;
        var initialHealth = 20;
        var maxHealth = 20;
        var staminaGrowth = 1;
        var healthGrowth = 2;
        
        
        switch (difficulty)
        {
            case GameDifficulty.Easy:
                waitSuccessRate = 1000;
                initialStamina = 20;
                staminaPerDay = 2;
                staminaGrowth = 2;
                healthGrowth = 3;
                break;
            case GameDifficulty.Normal:
                break;
            case GameDifficulty.Difficult:
                staminaPerDay = 4;
                initialHealth = 15;
                healthGrowth = 1;
                initialStamina = 10;
                staminaGrowth = 0;
                break;
        }

        return new Settings
        {
            WaitSuccessRate = waitSuccessRate,
            ExpPerDay = expPerDay,
            StaminaPerDay = staminaPerDay,
            InitialStamina = initialStamina,
            MaxStamina = maxStamina,
            InitialHealth = initialHealth,
            MaxHealth = maxHealth,
            StaminaGrowth = staminaGrowth,
            HealthGrowth = healthGrowth
        };
    }
}