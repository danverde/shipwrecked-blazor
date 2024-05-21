using FluentAssertions;
using Shared;
using Shipwrecked.Application.Factories;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Domain.Enums;

namespace Shipwrecked.Application.Test.Services;

public class PlayerServiceTests
{
    private readonly IPlayerService _service;

    public PlayerServiceTests()
    {
        _service = new PlayerService();
    }
    
    #region CreatePlayer

    [Fact]
    public void CreatePlayer_ValidInput_ShouldReturnPlayer()
    {
        // Arrange
        var name = "some name";
        var gender = Gender.Male;
        var difficulty = GameDifficulty.Normal;

        var expected = PlayerFactory.CreatePlayer(name, gender, difficulty);
        
        // Act
        var result = _service.CreatePlayer(name, gender, difficulty);
        
        // Assert
        result.Should().BeEquivalentTo(expected, opt => opt.Excluding(p => p.Id));
    }

    [Theory]
    [ClassData(typeof(NullAndWhitespaceStrings))]
    public void CreatePlayer_NullOrEmptyName_ShouldThrow(string name)
    {
        // Arrange
        Action act = () => _service.CreatePlayer(name, Gender.Female, GameDifficulty.Normal);
        
        // Act/Assert
        act.Should().Throw<ArgumentException>().WithParameterName("name");
    }

    #endregion
    
}