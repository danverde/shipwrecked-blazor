using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Interfaces;

/// <summary>
/// Interface for a factory used to create Game settings objects
/// </summary>
public interface ISettingsService
{
    /// <summary>
    /// Creates a <see cref="Settings"/> object
    /// that corresponds with the given <see cref="GameDifficulty"/>
    /// </summary>
    Settings GetSettings(GameDifficulty difficulty);
}