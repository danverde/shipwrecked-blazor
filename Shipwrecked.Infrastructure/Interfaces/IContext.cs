using Shipwrecked.Domain.Models;

namespace Shipwrecked.Infrastructure.Interfaces;

public interface IContext
{
    public State GetState();

    public void SetGameState(Game game);

    public void SetPlayerState(Player player);

}
