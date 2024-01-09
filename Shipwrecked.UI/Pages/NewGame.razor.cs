using Newtonsoft.Json;
using Shipwrecked.UI.Models;

namespace Shipwrecked.UI.Pages;

/// <summary>
/// Code behind for the New Game Page
/// </summary>
public partial class NewGame
{
    private NewGameInput FormInput = new NewGameInput();
    private void HandleFormSubmit()
    {
        Console.WriteLine("Handle Start Click Called");
        Console.WriteLine(JsonConvert.SerializeObject(FormInput));
    }
}