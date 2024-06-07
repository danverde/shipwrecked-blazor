using System.Diagnostics.CodeAnalysis;
using D = Shipwrecked.Domain.Models;

namespace Shipwrecked.UI.Store.Settings.Actions;

/// <summary>
/// Action triggered when configuring the game settings 
/// </summary>
[ExcludeFromCodeCoverage]
public class SetSettingsAction(D.Settings settings)
{
    public D.Settings Settings { get; set; } = settings;
}