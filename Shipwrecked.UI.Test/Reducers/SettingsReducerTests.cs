using FluentAssertions;
using Shared;
using Shipwrecked.Application.Actions;
using Shipwrecked.Domain.Models;
using Shipwrecked.UI.Store.Game.Actions;
using Shipwrecked.UI.Store.Settings;
using Shipwrecked.UI.Store.Settings.Actions;

namespace Shipwrecked.UI.Test.Reducers;

public class SettingsReducerTests
{
    [Fact]
    public void SetSettingsReducer_ValidParams_ShouldSetSettings()
    {
        // Arrange
        var settings = DomainFactory.CreateSettings();
        var state = new SettingsState(new Settings());
        var action = new SetSettingsAction(settings);

        var expectedState = new SettingsState(settings);
        
        // Act
        SettingsState result = SettingsReducer.SetSettingsReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(expectedState);
    }
    
    [Fact]
    public void QuitGameReducer_ValidParams_ShouldResetSettings()
    {
        // Arrange
        var settings = new Settings();
        var state = new SettingsState(DomainFactory.CreateSettings());
        var action = new QuitGameAction();

        var expectedState = new SettingsState(settings);
        
        // Act
        SettingsState result = SettingsReducer.QuitGameReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(expectedState);
    }
    
    [Fact]
    public void GameOverReducer_ValidParams_ShouldResetSettings()
    {
        // Arrange
        var settings = new Settings();
        var state = new SettingsState(DomainFactory.CreateSettings());
        var action = new GameOverAction();

        var expectedState = new SettingsState(settings);
        
        // Act
        SettingsState result = SettingsReducer.GameOverReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(expectedState);
    }
}