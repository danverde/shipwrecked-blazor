using Shipwrecked.Application.Enums;
using Shipwrecked.Domain.Models;

namespace Shipwrecked.Application.Factories;

public class GameFactory : IGameFactory
{
    public Game CreateGame(GameDifficulty difficulty)
    {
        Game game = new();
        switch (difficulty)
        {
            case GameDifficulty.Easy:
                break;
            case GameDifficulty.Normal:
                break;
            case GameDifficulty.Hard:
                break;
        }

        return game;
    }
}