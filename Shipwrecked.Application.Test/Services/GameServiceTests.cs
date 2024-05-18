using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure;
using Shipwrecked.Infrastructure.Interfaces;

namespace Shipwrecked.Application.Test.Services;

/// <summary>
/// Unit tests for the <see cref="GameService"/> class
/// </summary>
[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
public class GameServiceTests
{
    private readonly Mock<IGameSettingsFactory> _gameSettingsFactoryMock = new();
    private readonly Mock<IContext> _state = new();
    private readonly IGameService _service;

    /// <summary>
    /// Constructor that sets up each test run
    /// </summary>
    public GameServiceTests()
    {
        _state.Setup(x => x.GetState()).Returns(new State {Game = new Game()});
        
        _service = new GameService(_gameSettingsFactoryMock.Object, _state.Object);
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
        var store = param.Equals("gameStore") ? null! : _state.Object; 
        
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
        _state.Verify(x => x.SetGameState(It.Is<Game>(g => g.Difficulty == difficulty)), Times.AtLeastOnce);
        _gameSettingsFactoryMock.Verify(x => x.Create(difficulty), Times.AtLeastOnce);
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
        _service.StartGame(GameDifficulty.Normal);
        
        // Act
        _service.IncrementDay();
        
        // Assert
        _state.Verify(x => x.SetGameState(It.Is<Game>(g => g.Day == 1)), Times.AtLeastOnce);
    }

    #endregion
}