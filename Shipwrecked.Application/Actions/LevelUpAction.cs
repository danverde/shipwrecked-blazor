using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Actions;

/// <summary>
/// Action dispatched when a player levels up 
/// </summary>
[ExcludeFromCodeCoverage]
public class LevelUpAction
{
    public Player Player { get; set; }

    public LevelUpAction(Player player)
    {
        Player = Guard.Against.Null(player);
    }
}