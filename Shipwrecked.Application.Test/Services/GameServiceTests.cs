using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Fluxor;
using Moq;
using Shipwrecked.Application.Actions;
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
[SuppressMessage("Performance", "CA1806:Do not ignore method results")]
public class GameServiceTests
{
    private readonly Mock<IGameSettingsFactory> _gameSettingsFactoryMock = new();
    private readonly Mock<IDispatcher> _dispatcherMock = new();
    private readonly IGameService _service;

    /// <summary>
    /// Constructor that sets up each test run
    /// </summary>
    public GameServiceTests()
    {
        _service = new GameService(_dispatcherMock.Object, _gameSettingsFactoryMock.Object);
    }

    #region Constructor

    /// <summary>
    /// The Constructor should throw a <see cref="ArgumentNullException"/>
    /// if any of the parameters are null
    /// </summary>
    [Theory]
    [InlineData("dispatcher")]
    [InlineData("gameSettingsFactory")]
    public void Constructor_NullParam_ShouldThrow(string param)
    {
        // Arrange
        var dispatcher = param.Equals("dispatcher") ? null! : _dispatcherMock.Object; 
        var factory = param.Equals("gameSettingsFactory") ? null! : _gameSettingsFactoryMock.Object;
        
        // Act
        Action act = () => new GameService(dispatcher, factory);
        
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
        _service.StartNewGame(difficulty);
        
        // Assert
        _dispatcherMock.Verify(x => x.Dispatch(It.IsAny<LoadGameAction>()), Times.Once);
        _dispatcherMock.Verify(x => x.Dispatch(It.IsAny<GameLoadedAction>()), Times.Once);
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
        _service.StartNewGame(GameDifficulty.Normal);
        
        // Act
        _service.IncrementDay();
        
        // Assert
        _dispatcherMock.Verify(x => x.Dispatch(It.IsAny<IncrementDayAction>()), Times.Once);
    }

    #endregion
}