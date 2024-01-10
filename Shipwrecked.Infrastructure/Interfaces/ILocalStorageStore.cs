using Shipwrecked.Domain.Models;

namespace Shipwrecked.Infrastructure.Interfaces;

public interface ILocalStorageStore
{
    void SaveGame();
    Game LoadGame();
}