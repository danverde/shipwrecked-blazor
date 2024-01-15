using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
using Shipwrecked.Application.Context;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure.Interfaces;

namespace Shipwrecked.Application.Test.Context;

/// <summary>
/// Unit tests for the <see cref="GameService"/> class
/// </summary>
[SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
public class GameContextTests
{
    private readonly Mock<IGameSettingsFactory> _gameSettingsFactoryMock = new();
    private readonly IGameContext _context;

    /// <summary>
    /// Constructor that sets up each test run
    /// </summary>
    public GameContextTests()
    {
        _context = new GameContext(_gameSettingsFactoryMock.Object);
    }

    #region Constructor

    /// <summary>
    /// The Constructor should throw a <see cref="ArgumentNullException"/>
    /// if any of the parameters are null
    /// </summary>
    [Theory]
    [InlineData("gameStore")]
    public void Constructor_NullParam_ShouldThrow(string param)
    {
        // Arrange
        var factory = param.Equals("gameSettingsFactory") ? null! : _gameSettingsFactoryMock.Object;
        
        // Act
        Action act = () => new GameContext(factory);
        
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
        _context.StartGame(difficulty);
        
        // Assert
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
        int initialDay = _context.GetGame().Day;
        int expectedDay = initialDay + 1;
        
        // Act
        int result = _context.IncrementDay();
        
        // Assert
        result.Should().Be(expectedDay);
        _context.GetGame().Day.Should().Be(expectedDay);
    }

    #endregion
}