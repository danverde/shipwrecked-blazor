using Shipwrecked.Domain.Models;

namespace Shipwrecked.Infrastructure.Services;

public interface ILocalStorageService
{
    void SaveGame();
    Game LoadGame();
}