namespace Shipwrecked.Domain.Models;

/// <summary>
/// Lists available Game Statuses
/// </summary>
public enum GameStatus
{
    // Over,
    PendingSetup,
    Playing,
    Lost,
    Won,
    Quit,
}