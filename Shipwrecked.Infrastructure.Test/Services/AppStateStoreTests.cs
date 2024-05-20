using Blazored.LocalStorage;
using FluentAssertions;
using Moq;
using Shared;
using Shipwrecked.Infrastructure.Interfaces;
using Shipwrecked.Infrastructure.Models;
using Shipwrecked.Infrastructure.Services;

namespace Shipwrecked.Infrastructure.Test.Services;

/// <summary>
/// Unit Tests for the <see cref="AppStateStore"/> class
/// </summary>
public class AppStateStoreTests
{
    private readonly Mock<ILocalStorageService> _localStorageServiceMock;
    private readonly IAppStateStore _store;

    public AppStateStoreTests()
    {
        _localStorageServiceMock = new();
        _store = new AppStateStore(_localStorageServiceMock.Object);
    }

    #region Constructor

    [Fact]
    public void Constructor_NullLocalStorageService_ShouldThrow()
    {
        // Arrange
        Action act = () => new AppStateStore(null!);
        
        // Act/Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("localStorageService");
    }

    #endregion

    #region ExistsAsync

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ExistsAsync_ValidInput(bool exists)
    {
        // Arrange
        _localStorageServiceMock.Setup(x => x.ContainKeyAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(exists);
        
        // Act
        var result = await _store.ExistsAsync(Guid.NewGuid());
        
        // Assert
        result.Should().Be(exists);
        _localStorageServiceMock.Verify(x => x.ContainKeyAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task ExistsAsync_EmptyId_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _store.ExistsAsync(Guid.Empty);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    #endregion

    #region ListAppStateAsync

    [Fact]
    public async Task ListAppStatesAsync_ReturnsAllStates()
    {
        // Arrange
        var validKeys = new List<string>
        {
            $"sg:{Guid.NewGuid()}",
            $"sg:{Guid.NewGuid()}"
        };
        
        var allKeys = new List<string>
        {
            "sg:",
            Guid.NewGuid().ToString(),
            $"sg{Guid.NewGuid()}",
            $"mg:{Guid.NewGuid()}"
        };
        allKeys = allKeys.Concat(validKeys).ToList();

        _localStorageServiceMock.Setup(x => x.KeysAsync(It.IsAny<CancellationToken>())).ReturnsAsync(allKeys);
        _localStorageServiceMock.Setup(x => x.GetItemAsync<AppState>(It.Is<string>(k => validKeys.Contains(k)), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AppState());

        // Act
        IList<AppState> result = await _store.ListAppStatesAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(validKeys.Count);
        
        _localStorageServiceMock.Verify(x => x.KeysAsync(It.IsAny<CancellationToken>()), Times.Once);
        _localStorageServiceMock.Verify(x => x.GetItemAsync<AppState>(It.Is<string>(k => validKeys.Contains(k)), It.IsAny<CancellationToken>()), Times.Exactly(validKeys.Count));
    }
    
    [Fact]
    public async Task ListAppStatesAsync_NoStates_ReturnsEmptyList()
    {
        // Arrange
        var allKeys = new List<string>
        {
            "sg:",
            Guid.NewGuid().ToString(),
            $"sg{Guid.NewGuid()}",
            $"mg:{Guid.NewGuid()}"
        };

        _localStorageServiceMock.Setup(x => x.KeysAsync(It.IsAny<CancellationToken>())).ReturnsAsync(allKeys);
        _localStorageServiceMock.Setup(x => x.GetItemAsync<AppState>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AppState());

        // Act
        IList<AppState> result = await _store.ListAppStatesAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
        
        _localStorageServiceMock.Verify(x => x.KeysAsync(It.IsAny<CancellationToken>()), Times.Once);
        _localStorageServiceMock.Verify(x => x.GetItemAsync<AppState>(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region LoadAsync

    [Fact]
    public async Task LoadAsync_ValidId_StateExists_ShouldReturnAppState()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expected = new AppState
        {
            Game = DomainFactory.CreateGame(id)
        };

        _localStorageServiceMock.Setup(x => x.GetItemAsync<AppState>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);
        
        // Act
        AppState? result = await _store.LoadAsync(id);
        
        // Assert
        result.Should().BeEquivalentTo(expected);
        _localStorageServiceMock.Verify(x => x.GetItemAsync<AppState>(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task LoadAsync_ValidId_StateMissing_ShouldReturnNull()
    {
        // Arrange
        _localStorageServiceMock.Setup(x =>
            x.GetItemAsync<AppState>(It.IsAny<string>(), It.IsAny<CancellationToken>()));
        
        // Act
        AppState? result = await _store.LoadAsync(Guid.NewGuid());
        
        // Assert
        result.Should().BeNull();
    }
    
    [Fact]
    public async Task LoadAsync_EmptyId_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _store.LoadAsync(Guid.Empty);
        
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
        AppState appState = new AppState
        {
            Game = DomainFactory.CreateGame(id)
        };

        _localStorageServiceMock.Setup(x =>
            x.SetItemAsync(It.IsAny<string>(), It.IsAny<AppState>(), It.IsAny<CancellationToken>()));
        
        // Act
        await _store.SaveAsync(appState);
        
        // Assert
        _localStorageServiceMock.Verify(x =>
            x.SetItemAsync(It.IsAny<string>(), It.IsAny<AppState>(), It.IsAny<CancellationToken>()), Times.Once);

    } 
    
    [Fact]
    public async Task SaveAsync_NullAppState_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _store.SaveAsync(null!);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("appState");
    }

    #endregion

    #region DeleteAsync

    [Fact]
    public async Task DeleteAsync_ValidId_ShouldDelete()
    {
        // Arrange
        var id = Guid.NewGuid();

        _localStorageServiceMock.Setup(x => x.RemoveItemAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()));
        
        // Act
        await _store.DeleteAsync(id);
        
        // Assert
        _localStorageServiceMock.Verify(x => x.RemoveItemAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task DeleteAsync_EmptyId_ShouldThrow()
    {
        // Arrange
        Func<Task> act = async () => await _store.DeleteAsync(Guid.Empty);
        
        // Act/Assert
        await act.Should().ThrowAsync<ArgumentException>().WithParameterName("id");
    }

    #endregion
}