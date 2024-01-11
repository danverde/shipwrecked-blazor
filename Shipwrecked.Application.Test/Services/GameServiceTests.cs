using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure.Interfaces;

namespace Shipwrecked.Application.Test.Services;

/// <summary>
/// Unit tests for the <see cref="GameService"/> class
/// </summary>
[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
public class GameServiceTests
{
    private readonly Mock<IGameSettingsFactory> _gameSettingsFactoryMock = new();
    private readonly Mock<IGameStore> _gameStoreMock = new();
    private readonly IGameService _service;

    /// <summary>
    /// Constructor that sets up each test run
    /// </summary>
    public GameServiceTests()
    {
        _service = new GameService(_gameSettingsFactoryMock.Object, _gameStoreMock.Object);
    }

    #region Constructor

    /// <summary>
    /// The Constructor should throw a <see cref="ArgumentNullException"/>
    /// if any of the parameters are null
    /// </summary>
    [Theory]
    [InlineData("gameSettingsFactory")]
    [InlineData("gameStore")]
    public void Constructor_NullParam_ShouldThrow(string param)
    {
        // Arrange
        var factory = param.Equals("gameSettingsFactory") ? null! : _gameSettingsFactoryMock.Object;
        var store = param.Equals("gameStore") ? null! : _gameStoreMock.Object; 
        
        // Act
        Action act = () => new GameService(factory, store);
        
        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(param);
    }

    #endregion

    #region StartGame

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
        _gameSettingsFactoryMock.Setup(x => x.Create(difficulty)).Returns(new GameSettings());
        
        // Act
        _service.StartGame(difficulty);
        
        // Assert
        _gameStoreMock.Verify(x => x.UpdateGame(It.Is<Game>(g => g.Difficulty == difficulty)), Times.Once);
        _gameSettingsFactoryMock.Verify(x => x.Create(difficulty), Times.Once);
    }

    #endregion

    #region IncrementDay

    /// <summary>
    /// Verify that IncrementDay will increase the day by 1
    /// </summary>
    [Fact]
    public void IncrementDay_ShouldIncrementDay()
    {
        // Arrange
        
        // Act
        _service.IncrementDay();
        
        // Assert
        _gameStoreMock.Verify(x => x.UpdateGame(It.Is<Game>(g => g.Day == 1)), Times.Once);
    }

    #endregion
}