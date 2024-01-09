using Shipwrecked.Application.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Factories;

public interface IGameFactory
{
    Game CreateGame(GameDifficulty difficulty);
}