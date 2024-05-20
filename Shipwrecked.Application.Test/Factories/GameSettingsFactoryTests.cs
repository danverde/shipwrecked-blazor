using FluentAssertions;
using Shipwrecked.Application.Factories;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Enums;

namespace Shipwrecked.Application.Test.Factories;

/// <summary>
/// Unit tests for the <see cref="GameSettingsFactory"/> class
/// </summary>
public class GameSettingsFactoryTests
{
    private readonly IGameSettingsFactory _gameSettingsFactory;

    public GameSettingsFactoryTests()
    {
        _gameSettingsFactory = new GameSettingsFactory();
    }
    
    [Theory]
    [InlineData(GameDifficulty.Normal)]
    [InlineData(GameDifficulty.Easy)]
    [InlineData(GameDifficulty.Difficult)]
    public void Create_ShouldReturnGameSettings(GameDifficulty difficulty)
    {
        // Act
        var result = _gameSettingsFactory.Create(difficulty);
        
        // Assert
        result.Should().NotBeNull();

    }
}