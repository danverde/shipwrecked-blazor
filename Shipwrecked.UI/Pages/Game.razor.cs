using Microsoft.AspNetCore.Components;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure;

namespace Shipwrecked.UI.Pages;

public partial class Game
{
    [Parameter]
    public string Id { get; set; }

    [Inject] 
    private IGameService GameService { get; set; } = default!;

    private readonly Domain.Models.Game _gameContext = State.Game;

    private readonly Player _playerContext = State.Player;

    protected override void OnInitialized()
    {
        // TODO on page load look for a game with the corresponding ID in state...
        base.OnInitialized();
    }

    private void IncrementDay()
    {
        Console.WriteLine("Increment Day called");
        
        GameService.IncrementDay();
    }
}