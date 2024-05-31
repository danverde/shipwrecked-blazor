using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using D = Shipwrecked.Domain.Models;

namespace Shipwrecked.UI.Store.Game.Actions;

/// <summary>
/// Action triggered when a new game has finished loading
/// </summary>
[ExcludeFromCodeCoverage]
public class GameLoadedAction
{
    public D.Game Game { get; set; }

    public GameLoadedAction(D.Game game)
    {
        Game = Guard.Against.Null(game);
    }
}