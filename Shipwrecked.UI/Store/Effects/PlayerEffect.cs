using Ardalis.GuardClauses;
using Fluxor;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.UI.Store.Game.Actions;
using Shipwrecked.UI.Store.Player;

namespace Shipwrecked.UI.Store.Effects;

/// <summary>
/// Handles side effects that alter the player state
/// </summary>
public class PlayerEffect (IPlayerService playerService, IDispatcher dispatcher, IState<PlayerState> playerState)
{
    private readonly IPlayerService _playerService = Guard.Against.Null(playerService);
    private readonly IDispatcher _dispatcher = Guard.Against.Null(dispatcher);
    private readonly IState<PlayerState> _playerState = Guard.Against.Null(playerState);
    
    [EffectMethod]
    public Task IncrementDayEffectAsync(IncrementDayAction action, IDispatcher dispatcher)
    {
        var actions = _playerService.IncrementDay(_playerState.Value.Player);
        foreach (var a in actions)
        {
            _dispatcher.Dispatch(a);
        }
        
        return Task.CompletedTask;
    }
}