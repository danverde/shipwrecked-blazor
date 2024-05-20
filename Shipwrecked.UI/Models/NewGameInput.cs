using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Shipwrecked.Domain.Enums;

namespace Shipwrecked.UI.Models;

/// <summary>
/// Represents the Form submitted to start a new game
/// </summary>
[ExcludeFromCodeCoverage]
public class NewGameInput
{
    /// <summary>
    /// The Players Name
    /// TODO add validation for length!
    /// </summary>
    [Required]
    public string Name { get; set; } = "";

    /// <summary>
    /// The players gender
    /// </summary>
    public Gender Gender { get; set; } = Gender.Male;

    /// <summary>
    /// The game difficulty
    /// </summary>
    public GameDifficulty GameDifficulty { get; set; } = GameDifficulty.Normal;
}