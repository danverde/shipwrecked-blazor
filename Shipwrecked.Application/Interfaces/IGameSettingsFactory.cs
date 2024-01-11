using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Interfaces;

/// <summary>
/// Interface for a factory used to create Game settings objects
/// </summary>
public interface IGameSettingsFactory
{
    /// <summary>
    /// Creates a <see cref="GameSettings"/> object
    /// that corresponds with the given <see cref="GameDifficulty"/>
    /// </summary>
    GameSettings Create(GameDifficulty difficulty);
}