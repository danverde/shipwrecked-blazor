using Shipwrecked.Domain.Models;

namespace Shipwrecked.Infrastructure.Interfaces;

public interface IContext : IReadContext
{
    public void SetState(State state);
    
    public void SetGameState(Game game);

    public void SetPlayerState(Player player);

}
