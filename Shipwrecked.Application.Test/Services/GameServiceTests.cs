using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Fluxor;
using Moq;
using Shipwrecked.Application.Factories;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Test.Services;

/// <summary>
/// Unit tests for the <see cref="GameService"/> class
/// </summary>
[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
public class GameServiceTests
{
    private readonly Mock<IGameSettingsFactory> _gameSettingsFactoryMock = new();
    private readonly IGameService _service;

    /// <summary>
    /// Constructor that sets up each test run
    /// </summary>
    public GameServiceTests()
    {
        _service = new GameService(_gameSettingsFactoryMock.Object);
    }

    #region Constructor

    /// <summary>
    /// The Constructor should throw a <see cref="ArgumentNullException"/>
    /// if any of the parameters are null
    /// </summary>
    [Theory]
    [InlineData("gameSettingsFactory")]
    public void Constructor_NullParam_ShouldThrow(string param)
    {
        // Arrange
        var factory = param.Equals("gameSettingsFactory") ? null! : _gameSettingsFactoryMock.Object;
        
        // Act
        Action act = () => new GameService(factory);
        
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(param);
    }

    #endregion

    #region StartNewGame

    /// <summary>
    /// Verify that StartGame sends a new game to the IGame Store
    /// </summary>
    [Theory]
    [InlineData(GameDifficulty.Easy)]
    [InlineData(GameDifficulty.Normal)]
    [InlineData(GameDifficulty.Difficult)]
    public void StartGame_ValidParam_ShouldStartAGame(GameDifficulty difficulty)
    {
        // Arrange
        var gameSettings = new GameSettingsFactory().Create(difficulty);
        var expected = new Game
        {
            Difficulty = difficulty,
            Settings = gameSettings
        };
        
        _gameSettingsFactoryMock.Setup(x => x.Create(difficulty)).Returns(gameSettings);
        
        // Act
        Game result = _service.StartNewGame(difficulty);
        
        // Assert
        result.Should().BeEquivalentTo(expected, opt => opt.Excluding(g => g.Id));
        _gameSettingsFactoryMock.Verify(x => x.Create(difficulty), Times.AtLeastOnce);
    }

    #endregion
}