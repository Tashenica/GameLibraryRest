using GameLibraryApi.Models;

namespace GameLibraryApi.Interfaces
{
    public interface IGameService
    {
        GameInformation CreateGame(GameInformation gameInformation);
        void DeleteGame(int id);
        GameInformation EditGame(GameInformation gameInformation);
        List<GameInformation> GetAllGames();
        GameInformation GetGame(int id);
    }
}
