using FluentAssertions;
using Moq;
using Shared;
using Shipwrecked.Application.Actions;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Domain.Models;
using Shipwrecked.Infrastructure.Interfaces;
using Shipwrecked.Infrastructure.Models;

namespace Shipwrecked.Application.Test.Services;

/// <summary>
/// Unit tests for the <see cref="AppStateService"/> class
/// </summary>
public class AppStateServiceTests
{
    private readonly Mock<IAppStateStore> _appStateStoreMock = new();
    private readonly IAppStateService _service;

    public AppStateServiceTests()
    {
        _service = new AppStateService(_appStateStoreMock.Object);
    }

    #region Constructor

    [Fact]
    public void Constructor_NullAppStateStore_ShouldThrow()
    {
        // Arrange
        Action act = () => new AppStateService(null!);
        
        // Act/Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("appStateStore");
    }

    #endregion

    #region ExistsAsync

    [Fact]
    public async Task ExistsAsync_GameExists_ShouldReturnTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        _appStateStoreMock.Setup(x => x.ExistsAsync(id)).ReturnsAsync(true);

        // Act
        var result = await _service.ExistsAsync(id);

        // Assert
        result.Should().BeTrue();
        _appStateStoreMock.Verify(x => x.ExistsAsync(id), Times.Once);
    }
    
    [Fact]
    public async Task ExistsAsync_GameDoesntExist_ShouldReturnFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        _appStateStoreMock.Setup(x => x.ExistsAsync(id)).ReturnsAsync(false);

        // Act
        var result = await _service.ExistsAsync(id);

        // Assert
        result.Should().BeFalse();
        _appStateStoreMock.Verify(x => x.ExistsAsync(id), Times.Once);
    }

    [Fact]
    public async Task ExistsAsync_EmptyId_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _service.ExistsAsync(Guid.Empty);

        // Act/Assert
        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    #endregion

    #region ListAsync

    [Fact]
    public async Task ListAsync_ShouldReturnSavedAppStates()
    {
        // Arrange
        var appStates = new List<AppState>
        {
            new ()
        };
        _appStateStoreMock.Setup(x => x.ListAppStatesAsync()).ReturnsAsync(appStates);
        
        // Act
        IList<AppState> result = await _service.ListAsync();
        
        // Assert
        result.Should().BeEquivalentTo(appStates);
        _appStateStoreMock.Verify(x => x.ListAppStatesAsync(), Times.Once);
    }

    #endregion

    #region LoadAsync

    [Fact]
    public async Task LoadAsync_GameExists_ShouldReturnGame()
    {
        // Arrange
        var id = Guid.NewGuid();
        AppState appState = new AppState
        {
            Game = new Game
            {
                Id = id
            }
        };

        _appStateStoreMock.Setup(x => x.ExistsAsync(id)).ReturnsAsync(true);
        _appStateStoreMock.Setup(x => x.LoadAsync(id)).ReturnsAsync(appState);

        // Act
        AppState? result = await _service.LoadAsync(id);

        // Assert
        result.Should().BeEquivalentTo(appState);
        _appStateStoreMock.Verify(x => x.ExistsAsync(id), Times.Once);
        _appStateStoreMock.Verify(x => x.LoadAsync(id), Times.Once);
    }

    [Fact]
    public async Task LoadAsync_GameMissing_ShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        _appStateStoreMock.Setup(x => x.ExistsAsync(id)).ReturnsAsync(false);

        // Act
        AppState? result = await _service.LoadAsync(id);

        // Assert
        result.Should().BeNull();
        _appStateStoreMock.Verify(x => x.ExistsAsync(id), Times.Once);
    }

    [Fact]
    public async Task LoadAsync_EmptyId_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _service.LoadAsync(Guid.Empty);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    #endregion

    #region SaveAsync

    [Fact]
    public async Task SaveAsync_ValidInput_ShouldSave()
    {
        // Arrange
        var id = Guid.NewGuid();
        var game = DomainFactory.CreateGame(id);
        var action = new SaveGameAction(game);

        var expected = new AppState
        {
            Game = game
        };
        
        // Act
        AppState result = await _service.SaveAsync(id, action);
        
        // Assert
        result.Should().BeEquivalentTo(expected);
        _appStateStoreMock.Verify(x => x.SaveAsync(id, It.IsAny<AppState>()), Times.Once());
    }

    [Fact]
    public async Task SaveAsync_EmptyId_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _service.SaveAsync(Guid.Empty, new SaveGameAction(new Game()));
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }
    
    [Fact]
    public async Task SaveAsync_NullAction_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _service.SaveAsync(Guid.NewGuid(),null!);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("saveGameAction");
    }
    
    #endregion

    #region DeleteAsync

    [Fact]
    public async Task DeleteAsync_ValidId_ShouldWork()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        // Act
        await _service.DeleteAsync(id);
        
        // Assert
        _appStateStoreMock.Verify(x => x.DeleteAsync(id), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_EmptyId_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _service.DeleteAsync(Guid.Empty);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    #endregion
}