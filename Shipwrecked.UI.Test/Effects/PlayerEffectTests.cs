using FluentAssertions;
using Fluxor;
using Moq;
using Shared;
using Shipwrecked.Application.Actions;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Domain.Models;
using Shipwrecked.UI.Store.Effects;
using Shipwrecked.UI.Store.Game.Actions;
using Shipwrecked.UI.Store.Player;

namespace Shipwrecked.UI.Test.Effects;

/// <summary>
/// Unit tests for the <see cref="PlayerEffect"/> class
/// </summary>
public class PlayerEffectTests
{
    private readonly Mock<IPlayerService> _playerServiceMock = new();
    private readonly Mock<IDispatcher> _dispatcherMock = new();
    private readonly Mock<IState<PlayerState>> _playerStateMock = new();

    private readonly PlayerEffect _playerEffect;
    
    public PlayerEffectTests()
    {
        _playerEffect = new PlayerEffect(_playerServiceMock.Object, _dispatcherMock.Object, _playerStateMock.Object);
    }
    
    #region Constructor

    [Theory]
    [InlineData("playerService")]
    [InlineData("dispatcher")]
    [InlineData("playerState")]
    public void Constructor_NullParam_ShouldThrow(string param)
    {
        // Arrange
        var playerService = param == "playerService" ? null! : _playerServiceMock.Object;
        var dispatcher = param == "dispatcher" ? null! : _dispatcherMock.Object;
        var playerState = param == "playerState" ? null! : _playerStateMock.Object;
        
        // Act
        Action act = () => new PlayerEffect(playerService, dispatcher, playerState);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(param);
    }

    #endregion

    #region IncrementDayEffect

    [Fact]
    public async Task IncrementDayEffectAsync_ValidParams_ShouldDispatchAdditionalActions()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        _playerStateMock.Setup(x => x.Value).Returns(new PlayerState(player));
        
        var incrementDayAction = new IncrementDayAction(2);
        var setStaminaAction = new SetStaminaAction(5);
        var setExpAction = new SetExpAction(10);
        
        var expectedActions = new List<object> {setStaminaAction, setExpAction};

        _playerServiceMock.Setup(x => x.IncrementDay(It.IsAny<Player>())).Returns(expectedActions);

        // Act
        await _playerEffect.IncrementDayEffectAsync(incrementDayAction, _dispatcherMock.Object);

        // Assert
        _playerServiceMock.Verify(x => x.IncrementDay(It.IsAny<Player>()), Times.Once);
        
        _dispatcherMock.Verify(x => x.Dispatch(setStaminaAction), Times.Once);
        _dispatcherMock.Verify(x => x.Dispatch(setExpAction), Times.Once);
        _dispatcherMock.VerifyNoOtherCalls();
    }

    #endregion
}