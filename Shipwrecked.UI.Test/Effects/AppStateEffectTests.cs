using FluentAssertions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Moq;
using Shared;
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
    private readonly AlertService _alertService = new();
    private readonly Mock<NavigationManager> _navigationManagerMock = new();
    
    private readonly Mock<IDispatcher> _dispatcherMock = new();
    private readonly AppStateEffect _appStateEffect;

    public AppStateEffectTests()
    {
        _appStateEffect = new AppStateEffect(_appStateServiceMock.Object, _alertService, _navigationManagerMock.Object);
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
        var alertService = param == "alertService" ? null! : _alertService;
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
    
    /// <summary>
    /// TODO implement real failure steps!
    /// </summary>
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
        await act.Should().ThrowAsync<NotImplementedException>();
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
}