using FluentAssertions;
using Shared;
using Shipwrecked.Domain.Interfaces;
using Shipwrecked.Domain.Managers;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Domain.Test.Managers;

/// <summary>
/// Unit tests for the <see cref="PlayerManager"/> class
/// </summary>
public class PlayerManagerTests
{
    private readonly IPlayerManager _manager = new PlayerManager();

    #region DecreaseStamina

    [Theory]
    [InlineData(10, 7)]
    [InlineData(1, 0)]
    [InlineData(3, 0)]
    [InlineData(0, 0)]
    [InlineData(-2, 0)]
    public void DecreaseStamina_ValidPlayer_ShouldDecreaseStamina(int stamina, int expectedStamina)
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        var settings = DomainFactory.CreateSettings();
        player.Stamina = stamina;

        var expectedPlayer = Util.Clone(player);
        expectedPlayer.Stamina = expectedStamina;
        
        // Act
        Player result = _manager.DecreaseStamina(player, settings);
        
        // Assert
        result.Should().BeEquivalentTo(expectedPlayer);
    }

    [Theory]
    [InlineData("player")]
    [InlineData("settings")]
    public void DecreaseStamina_NullParams_ShouldThrow(string param)
    {
        // Arrange
        var player = param == "player" ? null! : DomainFactory.CreatePlayer();
        var settings = param == "settings" ? null! : DomainFactory.CreateSettings();
        
        Action act = () => _manager.DecreaseStamina(player, settings);
        
        // Act/assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(param);
    }

    [Fact]
    public void DecreaseStamina_ShouldBeAPureFunction()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        var settings = DomainFactory.CreateSettings();
        
        // Act
        var result = _manager.DecreaseStamina(player, settings);
        
        // Assert
        result.Id.Should().Be(player.Id);
        result.Should().NotBe(player);
    }

    #endregion

    #region IncreaseExp

    [Theory]
    [InlineData(0, 1, 25, 1)]
    [InlineData(99, 1, 24, 2)]
    [InlineData(75, 1, 0, 2)]
    public void IncreaseExp_ShouldSometimesLevelUp(int exp, int level, int expectedExp, int expectedLevel)
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        var settings = DomainFactory.CreateSettings();
        player.Experience = exp;
        player.Level = level;

        // Act
        Player result = _manager.IncreaseExp(player, settings);
        
        // Assert
        result.Experience.Should().Be(expectedExp);
        result.Level.Should().Be(expectedLevel);
    }

    [Fact]
    public void IncreaseExp_LevelUp_ShouldIncreaseStats()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        var settings = DomainFactory.CreateSettings();
        player.Experience = 99;
        player.Level = 1;

        var expectedPlayer = new Player
        {
            Id = player.Id,
            Name = player.Name,
            Level = 2,
            Experience = 24,
            Stamina = 11,
            MaxStamina = 21,
            Health = 11,
            MaxHealth = 21,
            Location = player.Location,
            Inventory = player.Inventory
        };
        
        // Act
        Player result = _manager.IncreaseExp(player, settings);
        
        // Assert
        result.Should().BeEquivalentTo(expectedPlayer);
    }
    
    [Theory]
    [InlineData("player")]
    [InlineData("settings")]
    public void IncreaseExp_NullParams_ShouldThrow(string param)
    {
        // Arrange
        var player = param == "player" ? null! : DomainFactory.CreatePlayer();
        var settings = param == "settings" ? null! : DomainFactory.CreateSettings();
        
        Action act = () => _manager.IncreaseExp(player, settings);
        
        // Act/assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(param);
    }

    [Fact]
    public void IncreaseExp_ShouldBeAPureFunction()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        var settings = DomainFactory.CreateSettings();
        
        // Act
        var result = _manager.IncreaseExp(player, settings);
        
        // Assert
        result.Id.Should().Be(player.Id);
        result.Should().NotBe(player);
    }

    #endregion
}