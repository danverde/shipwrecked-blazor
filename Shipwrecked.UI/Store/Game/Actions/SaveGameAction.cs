using System.Diagnostics.CodeAnalysis;
using D = Shipwrecked.Domain.Models;

namespace Shipwrecked.UI.Store.Game.Actions;

/// <summary>
/// Action triggered when saving a game
/// </summary>
[ExcludeFromCodeCoverage]
public class SaveGameAction(D.Game game, D.Player player)
{
    public D.Game Game { get; set; } = game;
    public D.Player Player { get; set; } = player;
}