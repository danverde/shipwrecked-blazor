using System.Diagnostics.CodeAnalysis;

namespace Shipwrecked.Domain.Models;

/// <summary>
/// Object representing the various game settings
/// </summary>
[ExcludeFromCodeCoverage]
public class Settings
{
    /// <summary>
    /// Chance that waiting a day will result in ending the game
    /// successfully
    /// </summary>
    public int WaitSuccessRate { get; set; }
    public int ExpPerDay { get; set; }
    public int StaminaPerDay { get; set; }
    public int InitialStamina { get; set; }
    public int MaxStamina { get; set; }
    public int InitialHealth { get; set; }
    public int MaxHealth { get; set; }
    public int StaminaGrowth { get; set; }
    public int HealthGrowth { get; set; }
}