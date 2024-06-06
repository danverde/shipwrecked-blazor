using FluentAssertions;
using Moq;
using Shared;
using Shipwrecked.Application.Actions;
using Shipwrecked.Application.Interfaces;
using Shipwrecked.Application.Services;
using Shipwrecked.Domain.Enums;
using Shipwrecked.Domain.Interfaces;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Test.Services;

/// <summary>
/// Unit tests for the <see cref="PlayerService"/> object
/// </summary>
public class PlayerServiceTests
{
    private readonly Mock<IPlayerManager> _playerManagerMock = new();
    
    private readonly IPlayerService _service;

    public PlayerServiceTests()
    {
        _service = new PlayerService(_playerManagerMock.Object);
    }
    
    #region CreatePlayer

    [Theory]
    [InlineData(GameDifficulty.Normal)]
    [InlineData(GameDifficulty.Easy)]
    [InlineData(GameDifficulty.Difficult)]
    public void CreatePlayer_ValidInput_ShouldReturnPlayer(GameDifficulty difficulty)
    {
        // Arrange
        var name = "some name";
        var gender = Gender.Male;

        // Act
        var result = _service.CreatePlayer(name, gender, difficulty);
        
        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(name);
    }

    [Theory]
    [ClassData(typeof(NullAndWhitespaceStrings))]
    public void CreatePlayer_NullOrEmptyName_ShouldThrow(string name)
    {
        // Arrange
        Action act = () => _service.CreatePlayer(name, Gender.Female, GameDifficulty.Normal);
        
        // Act/Assert
        act.Should().Throw<ArgumentException>().WithParameterName("name");
    }

    #endregion

    #region IncrementDay

    [Fact]
    public void IncrementDay_ShouldDecreaseStaminaAndIncreaseExp()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();

        var expectedActions = new List<object>
        {
            new SetStaminaAction(player.Stamina - 5),
            new SetExpAction(player.Experience)
        };
        
        _playerManagerMock.Setup(x => x.DecreaseStamina(It.IsAny<Player>())).Returns(new Player {Stamina = player.Stamina - 5});
        _playerManagerMock.Setup(x => x.IncreaseExp(It.IsAny<Player>())).Returns(new Player {Level = player.Level + 1});
        
        // Act
        var result = _service.IncrementDay(player);
        
        // Arrange
        result.Should().BeEquivalentTo(expectedActions);
        
        _playerManagerMock.Verify(x => x.DecreaseStamina(It.IsAny<Player>()), Times.Once);
        _playerManagerMock.Verify(x => x.IncreaseExp(It.IsAny<Player>()), Times.Once);
    }
    
    [Fact]
    public void IncrementDay_NoStamina_ShouldEndGame()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        player.Stamina = 0;

        var expectedActions = new List<object>
        {
            new SetStaminaAction(player.Stamina),
            new GameOverAction()
        };
        
        _playerManagerMock.Setup(x => x.DecreaseStamina(It.IsAny<Player>())).Returns(player);
        _playerManagerMock.Setup(x => x.IncreaseExp(It.IsAny<Player>())).Returns(new Player {Level = player.Level + 1});
        
        // Act
        List<object> result = _service.IncrementDay(player);
        
        // Arrange
        result.First().Should().BeEquivalentTo(expectedActions.First());
        result.Last().Should().BeOfType<GameOverAction>();
        
        _playerManagerMock.Verify(x => x.DecreaseStamina(player), Times.Once);
        _playerManagerMock.Verify(x => x.IncreaseExp(player), Times.Never);
    }
    
    /// <summary>
    /// TODO HAS A BUG WHERE LEVELING UP RESETS STAMINA!
    /// seems like the setStamina & setExp actions don't actually work.
    /// The change to the player takes place as part of the incrementDay action. 
    /// </summary>
    [Fact]
    public void IncrementDay_LevelUp_ShouldLevelUp()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        player.Experience = 90;
        
        _playerManagerMock.Setup(x => x.DecreaseStamina(It.IsAny<Player>())).Returns(new Player {Stamina = player.Stamina - 5});
        _playerManagerMock.Setup(x => x.IncreaseExp(It.IsAny<Player>())).Returns(new Player {Level = player.Level + 1});
        
        // Act
        List<object> result = _service.IncrementDay(player);
        
        // Assert
        result.First().Should().BeOfType<SetStaminaAction>();
        result.Last().Should().BeOfType<LevelUpAction>();
        
        _playerManagerMock.Verify(x => x.DecreaseStamina(It.IsAny<Player>()), Times.Once);
        _playerManagerMock.Verify(x => x.IncreaseExp(It.IsAny<Player>()), Times.Once);
    }
    
    [Fact]
    public void IncrementPlayer_NullPlayer_ShouldThrow()
    {
        // Arrange
        Action act = () => _service.IncrementDay(null!);
        
        // Act/Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("player");
    }

    #endregion
    
}