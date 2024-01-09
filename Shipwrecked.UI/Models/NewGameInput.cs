using Shipwrecked.Domain.Enums;

namespace Shipwrecked.UI.Models;

/// <summary>
/// Represents the Form submitted to start a new game
/// </summary>
public class NewGameInput
{
    public string Name { get; set; } = "";

    public Gender Gender { get; set; } = Gender.Male;

    public GameDifficulty GameDifficulty { get; set; } = GameDifficulty.Normal;
}