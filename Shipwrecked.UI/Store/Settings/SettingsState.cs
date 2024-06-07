using System.Diagnostics.CodeAnalysis;
using Fluxor;
using D = Shipwrecked.Domain.Models;

namespace Shipwrecked.UI.Store.Settings;

/// <summary>
/// Object representing the settings state
/// </summary>
[FeatureState]
[ExcludeFromCodeCoverage]
public class SettingsState
{
    public D.Settings Settings { get; set; } = new();

    /// <summary>
    /// Used for initial state
    /// </summary>
    private SettingsState() {}

    public SettingsState(D.Settings settings)
    {
        Settings = settings;
    }
}