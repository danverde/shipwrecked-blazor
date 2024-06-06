using FluentAssertions;
using Shared;
using Shipwrecked.Application.Actions;
using Shipwrecked.Domain.Models;
using Shipwrecked.UI.Store.Game.Actions;
using Shipwrecked.UI.Store.Player;
using Shipwrecked.UI.Store.Player.Actions;

namespace Shipwrecked.UI.Test.Reducers;

/// <summary>
/// Unit tests for the <see cref="PlayerReducer"/> class
/// </summary>
public class PlayerReducerTests
{
    [Fact]
    public void SetPlayerReducer_ShouldSetPlayer()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        var state = new PlayerState(null!);
        var action = new SetPlayerAction(player);

        var expected = new PlayerState(player);
        
        // Act
        var result = PlayerReducer.SetPlayerReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void LevelUpReducer_ShouldSetProperties()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();

        var level = 2;
        var health = 10;
        var maxHealth = 15;
        var stamina = 10;
        var maxStamina = 15;
        var experience = 0;
        
        
        var state = new PlayerState(player);
        var action = new LevelUpAction(level, experience, health, maxHealth, stamina, maxStamina);

        // Act
        PlayerState result = PlayerReducer.LevelUpReducer(state, action);

        // Assert
        result.Player.Level.Should().Be(level);
        result.Player.Experience.Should().Be(experience);
        result.Player.Stamina.Should().Be(stamina);
        result.Player.MaxStamina.Should().Be(maxStamina);
        result.Player.Health.Should().Be(health);
        result.Player.MaxHealth.Should().Be(maxHealth);
    }

    [Fact]
    public void SetStaminaReducer_ShouldUpdateStamina()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        player.Stamina = 10;
        var state = new PlayerState(player);
        var action = new SetStaminaAction(5);

        var expected = new PlayerState(player);
        expected.Player.Stamina = 5;
        
        // Act
        PlayerState result = PlayerReducer.SetStaminaReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void SetExperienceReducer_ShouldUpdateStamina()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        player.Experience = 90;
        var state = new PlayerState(player);
        var action = new SetExpAction(95);

        var expected = new PlayerState(player);
        expected.Player.Experience = 95;
        
        // Act
        PlayerState result = PlayerReducer.SetExpReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void QuitGameReducer_ShouldResetPlayer()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        var state = new PlayerState(player);
        var action = new QuitGameAction();

        var expectedState = new PlayerState(new Player());
        
        // Act
        var result = PlayerReducer.QuitGameReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(expectedState);
    }
    
    [Fact]
    public void GameOverReducer_ShouldResetPlayer()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        var state = new PlayerState(player);
        var action = new GameOverAction();

        var expectedState = new PlayerState(new Player());
        
        // Act
        var result = PlayerReducer.GameOverReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(expectedState);
    }
}