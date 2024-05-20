using FluentAssertions;
using Fluxor;
using Moq;
using Shared;
using Shipwrecked.Application.Actions;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Infrastructure.Models;
using Shipwrecked.UI.Store.Effects;

namespace Shipwrecked.UI.Test.Effects;

/// <summary>
/// Unit tests for the <see cref="GameEffect"/> class
/// </summary>
public class GameEffectTests
{
    private readonly Mock<IAppStateService> _appStateServiceMock = new();
    private readonly Mock<IDispatcher> _dispatcherMock = new();
    private readonly GameEffect _gameEffect;

    public GameEffectTests()
    {
        _gameEffect = new GameEffect(_appStateServiceMock.Object);
    }

    #region Constructor

    [Fact]
    public void Constructor_NullAppStateService_ShouldThrow()
    {
        // Arrange
        Action act = () => new GameEffect(null!);
        
        // Act/Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("appStateService");
    }

    #endregion

    #region LoadGameEffectAsync

    [Fact]
    public async Task LoadGameEffectAsync_ValidInput_ExistingGame_ShouldLoad()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var loadAction = new LoadGameAction(gameId);
        var appState = new AppState
        {
            Game = DomainFactory.CreateGame(gameId)
        };

        _appStateServiceMock.Setup(x => x.LoadAsync(gameId)).ReturnsAsync(appState);
        
        // Act
        await _gameEffect.LoadAppStateEffectAsync(loadAction, _dispatcherMock.Object);
        
        // Assert
        _appStateServiceMock.Verify(x => x.LoadAsync(gameId), Times.Once);
        _dispatcherMock.Verify(x => x.Dispatch(It.Is<GameLoadedAction>(action => action.Game.Id == gameId)), Times.Once);
    }
    
    [Fact]
    public async Task LoadGameEffectAsync_ValidInput_MissingGame_ShouldThrow()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var loadAction = new LoadGameAction(gameId);

        _appStateServiceMock.Setup(x => x.LoadAsync(gameId));
        
        // Act
        Func<Task> act = async () => await _gameEffect.LoadAppStateEffectAsync(loadAction, _dispatcherMock.Object);
        
        // Assert
        await act.Should().ThrowAsync<NotImplementedException>();
    }
    
    [Fact]
    public async Task LoadGameEffectAsync_NullAction_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _gameEffect.LoadAppStateEffectAsync(null!, _dispatcherMock.Object);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("action");
    }
    
    [Fact]
    public async Task LoadGameEffectAsync_NullDispatcher_ShouldThrow()
    {
        // Arrange
        var action = new LoadGameAction(Guid.NewGuid());
        Func<Task> act = async () => await _gameEffect.LoadAppStateEffectAsync(action, null!);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("dispatcher");
    }  

    #endregion

    #region SaveGameEffectAsync

    [Fact]
    public async Task SaveGameEffectAsync_ValidInput_ShouldDispatchGameLoaded()
    {
        // Arrange
        var game = DomainFactory.CreateGame();
        var saveAction = new SaveGameAction(game);
        var appState = new AppState
        {
            Game = game
        };

        _appStateServiceMock.Setup(x => x.SaveAsync(saveAction)).ReturnsAsync(appState);
        
        // Act
        await _gameEffect.SaveAppStateEffectAsync(saveAction, _dispatcherMock.Object);
        
        // Assert
        _appStateServiceMock.Verify(x => x.SaveAsync(saveAction), Times.Once);
        _dispatcherMock.Verify(x => x.Dispatch(It.Is<GameLoadedAction>(action => action.Game.Id == game.Id)), Times.Once);
    }
    
    [Fact]
    public async Task SaveGameEffectAsync_NullAction_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _gameEffect.SaveAppStateEffectAsync(null!, _dispatcherMock.Object);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("action");
    }
    
    [Fact]
    public async Task SaveGameEffectAsync_NullDispatcher_ShouldThrow()
    {
        // Arrange
        var action = new SaveGameAction(DomainFactory.CreateGame());
        Func<Task> act = async () => await _gameEffect.SaveAppStateEffectAsync(action, null!);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("dispatcher");
    }  

    #endregion
}