using Fluxor;
using Shipwrecked.Application.Actions;
using Shipwrecked.UI.Store.Game.Actions;
using Shipwrecked.UI.Store.Settings.Actions;
using D = Shipwrecked.Domain.Models;

namespace Shipwrecked.UI.Store.Settings;

/// <summary>
/// Reducers for the Settings state
/// </summary>
public static class SettingsReducer
{
    [ReducerMethod]
    public static SettingsState SetSettingsReducer(SettingsState state, SetSettingsAction action) =>
        new SettingsState(action.Settings);
    
    [ReducerMethod]
    public static SettingsState QuitGameReducer(SettingsState state, QuitGameAction action) =>
        new SettingsState(new D.Settings());
    
    [ReducerMethod]
    public static SettingsState GameOverReducer(SettingsState state, GameOverAction action) =>
        new SettingsState(new D.Settings());
}