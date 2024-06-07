using Ardalis.GuardClauses;
using Fluxor;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.UI.Store.Game.Actions;
using Shipwrecked.UI.Store.Settings.Actions;

namespace Shipwrecked.UI.Store.Effects;

/// <summary>
/// Settings Side effects
/// </summary>
public class SettingsEffect
{
    private readonly ISettingsService _settingsService;

    public SettingsEffect(ISettingsService settingsService)
    {
        _settingsService = Guard.Against.Null(settingsService);
    }

    [EffectMethod]
    public Task GameLoadingEffect(GameLoadedAction action, IDispatcher dispatcher)
    {
        var settings = _settingsService.GetSettings(action.Game.Difficulty);
        dispatcher.Dispatch(new SetSettingsAction(settings));

        return Task.CompletedTask;
    }
}