using FluentAssertions;
using Shared;
using Shipwrecked.Application.Factories;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Test.Services;

public class PlayerServiceTests
{
    private readonly IPlayerService _service;

    public PlayerServiceTests()
    {
        _service = new PlayerService();
    }
    
    #region CreatePlayer

    [Theory]
    [InlineData(GameDifficulty.Normal)]
    [InlineData(GameDifficulty.Easy)]
    [InlineData(GameDifficulty.Difficult)]
    public void CreatePlayer_ValidInput_ShouldReturnPlayer(GameDifficulty difficulty)
    {
        // Arrange
        var name = "some name";
        var gender = Gender.Male;

        // Act
        var result = _service.CreatePlayer(name, gender, difficulty);
        
        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(name);
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