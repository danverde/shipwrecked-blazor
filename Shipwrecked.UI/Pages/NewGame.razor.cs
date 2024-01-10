using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using Shipwrecked.UI.Models;

namespace Shipwrecked.UI.Pages;

/// <summary>
/// Code behind for the New Game Page
/// </summary>
public partial class NewGame
{
    private const string FormId = "new-game-form";

    private EditContext? FormContext { get; set; }

    private readonly NewGameInput _formInput = new NewGameInput();
    private void HandleFormSubmit()
    {
        Console.WriteLine("Handle Start Click Called");
        Console.WriteLine(JsonConvert.SerializeObject(_formInput));
    }

    protected override void OnInitialized()
    {
        FormContext = new EditContext(_formInput);
    }
}