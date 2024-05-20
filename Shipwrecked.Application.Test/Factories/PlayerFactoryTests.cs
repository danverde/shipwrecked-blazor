using FluentAssertions;
using Shipwrecked.Application.Factories;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Test.Factories;

public class PlayerFactoryTests
{
    // TODO convert to class data
    [Theory]
    [InlineData(GameDifficulty.Normal)]
    [InlineData(GameDifficulty.Easy)]
    [InlineData(GameDifficulty.Difficult)]
    public void CreatePlayer_ValidInput_ShouldReturnAPlayer(GameDifficulty difficulty)
    {
        // Arrange
        var name = "name";
        
        // Act
        Player result = PlayerFactory.CreatePlayer("name", Gender.Male, difficulty);
        
        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(name);
        result.Id.Should().NotBeEmpty();
        result.Level.Should().Be(1);
        result.Experience.Should().Be(0);
        result.Inventory.Should().NotBeNull();
    }
    
    
    // TODO switch to class data!
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\n")]
    [InlineData("\t")]
    [InlineData(null)]
    public void CreatePlayer_InvalidName_ShouldThrow(string name)
    {
        // Arrange
        Action act = () => PlayerFactory.CreatePlayer(name, Gender.Male, GameDifficulty.Normal);
        
        // Act/Assert
        act.Should().Throw<ArgumentException>().WithParameterName("name");
    }
}