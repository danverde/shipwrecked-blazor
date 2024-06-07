using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
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
    private readonly IGameService _service;

    /// <summary>
    /// Constructor that sets up each test run
    /// </summary>
    public GameServiceTests()
    {
        _service = new GameService();
    }

    #region StartNewGame

    [Theory]
    [InlineData(GameDifficulty.Easy)]
    [InlineData(GameDifficulty.Normal)]
    [InlineData(GameDifficulty.Difficult)]
    public void StartGame_ValidParams_ShouldCreateAGame(GameDifficulty difficulty)
    {
        // Arrange
        var expected = new Game
        {
            Day = 1,
            Difficulty = difficulty
        };
        
        // Act
        Game result = _service.CreateGame(difficulty);
        
        // Assert
        result.Should().BeEquivalentTo(expected, opt => opt.Excluding(g => g.Id));
    }

    #endregion
}