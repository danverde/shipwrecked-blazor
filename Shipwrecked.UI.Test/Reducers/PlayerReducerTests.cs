using FluentAssertions;
using Shared;
using Shipwrecked.UI.Store.Player;
using Shipwrecked.UI.Store.Player.Actions;

namespace Shipwrecked.UI.Test.Reducers;

/// <summary>
/// Unit tests for the <see cref="PlayerReducer"/> class
/// </summary>
public class PlayerReducerTests
{
    [Fact]
    public void SetPlayerReducer_ShouldSetPlayer()
    {
        // Arrange
        var player = DomainFactory.CreatePlayer();
        var state = new PlayerState(null!);
        var action = new SetPlayerAction(player);

        var expected = new PlayerState(player);
        
        // Act
        var result = PlayerReducer.SetPlayerReducer(state, action);
        
        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}