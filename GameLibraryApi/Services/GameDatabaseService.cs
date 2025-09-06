using GameLibraryApi.Data;
using GameLibraryApi.Interfaces;
using GameLibraryApi.Models;

namespace GameLibraryApi.Services
{
    public class GameDatabaseService : IGameService
    {
        private GamingLibraryContext _context;
        public GameDatabaseService(GamingLibraryContext gamingLibraryContext)
        {
            _context = gamingLibraryContext;
        }

        public GameInformation CreateGame(GameInformation gameInformation)
        {
            _context.GameInformations.Add(gameInformation);
            _context.SaveChanges();

            return gameInformation;
        }

        public void DeleteGame(int id)
        {
            GameInformation gameInfo = GetGame(id);
            _context.Remove(gameInfo);
            _context.SaveChanges();
        }

        public GameInformation EditGame(GameInformation gameInformation)
        {
            GameInformation gameInfo = GetGame(gameInformation.Id);
            if (gameInfo != null)
            {
                gameInfo.Title = gameInformation.Title;
                gameInfo.GameType = gameInformation.GameType;
                gameInfo.CompanyName = gameInformation.CompanyName;
                gameInfo.Genre = gameInformation.Genre;
                gameInfo.AgeRestriction = gameInformation.AgeRestriction;
                gameInfo.Multiplayer = gameInformation.Multiplayer;
                gameInfo.Description = gameInformation.Description;
                gameInfo.Image = gameInformation.Image;
                gameInfo.YearPublished = gameInformation.YearPublished;

                _context.SaveChanges();
            }

            return gameInfo;
        }

        public List<GameInformation> GetAllGames()
        {
           return _context.GameInformations.ToList();
        }

        public GameInformation GetGame(int id)
        {
           return _context.GameInformations.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
