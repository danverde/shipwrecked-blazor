using System.Diagnostics.CodeAnalysis;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Actions;

/// <summary>
/// Action triggered when saving a game
/// </summary>
[ExcludeFromCodeCoverage]
public class SaveGameAction(Game game)
{
    public Game Game { get; set; } = game;
    // TODO add player, map, inventory, etc.
    // public Player Player { get; set; } = player;
}