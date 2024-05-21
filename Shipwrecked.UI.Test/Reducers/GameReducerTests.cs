using FluentAssertions;
using Shared;
using Shipwrecked.Domain.Models;
using Shipwrecked.UI.Store.Game;
using Shipwrecked.UI.Store.Game.Actions;

namespace Shipwrecked.UI.Test.Reducers;

public class GameReducerTests
{
    #region LoadGameReducer

    [Fact]
    public void LoadGameReducer_ShouldWork()
    {
        // Arrange
        var state = new GameState(false, true, new Game());
        var action = new LoadGameAction(Guid.NewGuid());
        
        var expected = new GameState(true, false, null);
        
        // Act
        GameState result = GameReducer.LoadGameReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    #endregion

    #region StartGameReducer

    [Fact]
    public void StartGameReducer_ShouldWork()
    {
        // Arrange
        var state = new GameState(false, true, new Game());
        var action = new StartGameAction();
        
        var expected = new GameState(true, false, null);
        
        // Act
        GameState result = GameReducer.StartGameReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    #endregion

    #region GameLoadedReducer

    [Fact]
    public void GameLoadedReducer_ShouldWork()
    {
        // Arrange
        var game = DomainFactory.CreateGame();
        var state = new GameState(true, false, null);
        var action = new GameLoadedAction(game);

        var expected = new GameState(false, true, game);
        
        // Act
        GameState result = GameReducer.GameLoadedReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(result);
    }

    #endregion

    #region QuitGameReducer

    [Fact]
    public void QuitGameReducer_ShouldWork()
    {
        // Arrange
        var game = DomainFactory.CreateGame();
        var state = new GameState(false, true, game);
        var action = new QuitGameAction();

        var expected = new GameState(false, false, null);
        
        // Act
        GameState result = GameReducer.QuitGameReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    #endregion

    #region IncrementDayReducer

    [Fact]
    public void IncrementDayReducer_ShouldWork()
    {
        // Arrange
        var game = DomainFactory.CreateGame();
        var state = new GameState(false, true, game);
        var action = new IncrementDayAction(game.Day + 1);

        var expectedGame = DomainFactory.CreateGame();
        expectedGame.Id = game.Id;
        expectedGame.Day++;
        var expected = new GameState(false, true, expectedGame);

        // Act
        GameState result = GameReducer.IncrementDayReducer(state, action);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    #endregion
}