using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Infrastructure;
using Shipwrecked.UI.Models;

namespace Shipwrecked.UI.Pages;

/// <summary>
/// Code behind for the New Game Page
/// </summary>
public partial class NewGame
{
    #region DI

    [Inject] 
    private IGameService GameService { get; set; } = default!;
    
    [Inject] 
    private IPlayerService PlayerService { get; set; } = default!;

    [Inject] private NavigationManager NavManager { get; set; } = default!;
    
    #endregion
    
    private const string FormId = "new-game-form";

    private EditContext? FormContext { get; set; }

    private readonly NewGameInput _formInput = new NewGameInput();

    protected override void OnInitialized()
    {
        FormContext = new EditContext(_formInput);
    }
    
    private void HandleFormSubmit()
    {
        GameService.StartGame(_formInput.GameDifficulty);
        PlayerService.CreatePlayer(_formInput.Name, _formInput.Gender, _formInput.GameDifficulty);

        var gameId = State.Game.Id;
        
        NavManager.NavigateTo($"/game/{gameId}");
    }
}