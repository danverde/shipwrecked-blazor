using Ardalis.GuardClauses;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure.Interfaces;

namespace Shipwrecked.Infrastructure;

/// <summary>
/// Stores the state of the entire application
/// (seems like a bad idea, right?)
/// </summary>
public class Context : IContext
{
    private State State { get; set; } = new();

    public State GetState() => State;

    public void SetState(State state)
    {
        Guard.Against.Null(state);

        State = state;
    }
    
    public void SetGameState(Game game)
    {
        Guard.Against.Null(game);

        State.Game = game;
    }

    public void SetPlayerState(Player player)
    {
        Guard.Against.Null(player);

        State.Player = player;
    }
}