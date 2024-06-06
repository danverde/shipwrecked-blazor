using FluentAssertions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Moq;
using Shared;
using Shipwrecked.Application.Actions;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Infrastructure.Models;
using Shipwrecked.UI.Services;
using Shipwrecked.UI.Store.Effects;
using Shipwrecked.UI.Store.Game.Actions;

namespace Shipwrecked.UI.Test.Effects;

/// <summary>
/// Unit tests for the <see cref="AppStateEffect"/> class
/// </summary>
public class AppStateEffectTests
{
    private readonly Mock<IAppStateService> _appStateServiceMock = new();
    private readonly Mock<IAlertService> _alertServiceMock = new();
    private readonly Mock<NavigationManager> _navigationManagerMock = new();
    
    private readonly Mock<IDispatcher> _dispatcherMock = new();
    private readonly AppStateEffect _appStateEffect;

    public AppStateEffectTests()
    {
        _appStateEffect = new AppStateEffect(_appStateServiceMock.Object, _alertServiceMock.Object, _navigationManagerMock.Object);
    }

    #region Constructor

    [Theory]
    [InlineData("appStateService")]
    [InlineData("alertService")]
    [InlineData("navManager")]
    public void Constructor_NullAppStateService_ShouldThrow(string param)
    {
        // Arrange
        var appStateService = param == "appStateService" ? null! : _appStateServiceMock.Object;
        var alertService = param == "alertService" ? null! : _alertServiceMock.Object;
        var navManager = param == "navManager" ? null! : _navigationManagerMock.Object;
        
        Action act = () => new AppStateEffect(appStateService, alertService, navManager);
        
        // Act/Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(param);
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
        await _appStateEffect.LoadAppStateEffectAsync(loadAction, _dispatcherMock.Object);
        
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
        Func<Task> act = async () => await _appStateEffect.LoadAppStateEffectAsync(loadAction, _dispatcherMock.Object);
        
        // Assert
        await act.Should().NotThrowAsync();
        _alertServiceMock.Verify(x => x.Error("Unable to load Game"), Times.Once);
    }
    
    [Fact]
    public async Task LoadGameEffectAsync_NullAction_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _appStateEffect.LoadAppStateEffectAsync(null!, _dispatcherMock.Object);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("action");
    }
    
    [Fact]
    public async Task LoadGameEffectAsync_NullDispatcher_ShouldThrow()
    {
        // Arrange
        var action = new LoadGameAction(Guid.NewGuid());
        Func<Task> act = async () => await _appStateEffect.LoadAppStateEffectAsync(action, null!);
        
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
        var player = DomainFactory.CreatePlayer();
        var saveAction = new SaveGameAction(game, player);
        var appState = new AppState
        {
            Game = game,
            Player = player
        };

        _appStateServiceMock.Setup(x => x.SaveAsync(game, player)).ReturnsAsync(appState);
        
        // Act
        await _appStateEffect.SaveAppStateEffectAsync(saveAction, _dispatcherMock.Object);
        
        // Assert
        _appStateServiceMock.Verify(x => x.SaveAsync(game, player), Times.Once);
        _dispatcherMock.Verify(x => x.Dispatch(It.Is<GameLoadedAction>(action => action.Game.Id == game.Id)), Times.Once);
        _alertServiceMock.Verify(x => x.Success("Game Saved"), Times.Once);
    }
    
    [Fact]
    public async Task SaveGameEffectAsync_NullAction_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _appStateEffect.SaveAppStateEffectAsync(null!, _dispatcherMock.Object);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("action");
    }
    
    [Fact]
    public async Task SaveGameEffectAsync_NullDispatcher_ShouldThrow()
    {
        // Arrange
        var action = new SaveGameAction(DomainFactory.CreateGame(), DomainFactory.CreatePlayer());
        Func<Task> act = async () => await _appStateEffect.SaveAppStateEffectAsync(action, null!);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("dispatcher");
    }  

    #endregion

    #region GameOverEffectAsync

    [Fact (Skip = "needs to be converted to bUnit to mock nav manager")]
    public async Task GameOverAction_ShouldCreateAlert()
    {
        // Arrange
        // _navigationManagerMock.Setup(x => x.NavigateTo(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()));

        // Act
        await _appStateEffect.GameOverEffectAsync(new GameOverAction(), _dispatcherMock.Object);
        
        // Assert
        // _alertServiceMock.Verify(x => x.Error("The game ended. You died"), Times.Once);
        _navigationManagerMock.Verify(x => x.NavigateTo("/", It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
    }
    
    [Theory]
    [InlineData("action")]
    [InlineData("dispatcher")]
    public async Task GameOverAction_NullParams_ShouldThrow(string param)
    {
        // Arrange
        var action = param == "action" ? null! : new GameOverAction();
        var dispatcher = param == "dispatcher" ? null! : _dispatcherMock.Object;

        // Act
        Func<Task> act = async () => await _appStateEffect.GameOverEffectAsync(action, dispatcher);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName(param);
    }

    #endregion
}