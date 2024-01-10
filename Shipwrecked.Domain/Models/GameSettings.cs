namespace Shipwrecked.Domain.Models;

/// <summary>
/// Object representing the various game settings
/// </summary>
public class GameSettings
{
    /// <summary>
    /// Chance that waiting a day will result in ending the game
    /// successfully
    /// </summary>
    public int WaitSuccessRate { get; set; }
    
    /// <summary>
    /// The amount of stamina consumed each day
    /// </summary>
    public int StaminaPerDay { get; set; }
}