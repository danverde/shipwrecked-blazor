using FluentAssertions;
using Shared;
using Shipwrecked.Application.Actions;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Managers;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Test.Services;

/// <summary>
/// Unit tests for the <see cref="PlayerService"/> object
/// </summary>
public class PlayerServiceTests
{
    private readonly IPlayerService _service;

    public PlayerServiceTests()
    {
        var playerManager = new PlayerManager();
        
        _service = new PlayerService(playerManager);
    }
    
    #region CreatePlayer

    [Fact]
    public void CreatePlayer_ValidInput_ShouldReturnPlayer()
    {
        // Arrange
        var name = "some name";
        var gender = Gender.Male;
        var settings = DomainFactory.CreateSettings();

        // Act
        var result = _service.CreatePlayer(name, gender, settings);
        
        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(name);
    }

    [Theory]
    [ClassData(typeof(NullAndWhitespaceStrings))]
    public void CreatePlayer_NullOrEmptyName_ShouldThrow(string name)
    {
        // Arrange
        Action act = () => _service.CreatePlayer(name, Gender.Female, new Settings());
        
        // Act/Assert
        act.Should().Throw<ArgumentException>().WithParameterName("name");
    }

    [Fact]
    public void CreatePlayer_NullSettings_ShouldThrow()
    {
        // Arrange
        Action act = () => _service.CreatePlayer("name", Gender.Female, null!);
        
        // Act/Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("settings");
    }

    #endregion

    #region IncrementDay

    [Fact]
    public void IncrementDay_ShouldDecreaseStaminaAndIncreaseExp()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();

        var expectedActions = new List<object>
        {
            new SetStaminaAction(7),
            new SetExpAction(25)
        };
        
        // Act
        var result = _service.IncrementDay(player);
        
        // Arrange
        result.Should().BeEquivalentTo(expectedActions);
    }
    
    [Fact]
    public void IncrementDay_NoStamina_ShouldEndGame()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        player.Stamina = 0;

        var expectedActions = new List<object>
        {
            new SetStaminaAction(player.Stamina),
            new GameOverAction()
        };
        
        // Act
        List<object> result = _service.IncrementDay(player);
        
        // Arrange
        result.First().Should().BeEquivalentTo(expectedActions.First());
        result.Last().Should().BeOfType<GameOverAction>();
    }
    
    [Fact]
    public void IncrementDay_LevelUp_ShouldLevelUp()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        player.Experience = 90;
        
        // Act
        List<object> result = _service.IncrementDay(player);
        
        // Assert
        result.First().Should().BeOfType<SetStaminaAction>();
        result.Last().Should().BeOfType<LevelUpAction>();
    }
    
    [Fact]
    public void IncrementPlayer_NullPlayer_ShouldThrow()
    {
        // Arrange
        Action act = () => _service.IncrementDay(null!);
        
        // Act/Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("player");
    }

    #endregion
    
}