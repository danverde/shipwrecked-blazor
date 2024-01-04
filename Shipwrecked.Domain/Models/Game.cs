namespace Shipwrecked.Domain.Models;

/// <summary>
/// Represents a Game object
/// </summary>
public class Game
{
    public Guid Id { get; set; }
    public Player Player { get; set; }
    public int Day { get; set; }
    public GameStatus Status { get; set; } = GameStatus.PendingSetup;
    public Map Map { get; set; }
    public GameSettings GameSettings { get; set; }
    public string SaveFileName { get; set; } = "";
}