using GameLibraryApi.Data;
using GameLibraryApi.Interfaces;
using GameLibraryApi.Models;
using Microsoft.EntityFrameworkCore;

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
            GameInformation? gameInfo = GetGame(gameInformation.Id);
            if (gameInfo != null)
            {
                gameInfo.Title = gameInformation.Title;
                gameInfo.GameTypeId = gameInformation.GameTypeId;
                gameInfo.GenreId = gameInformation.GenreId;
                gameInfo.CompanyName = gameInformation.CompanyName;
                gameInfo.AgeRestrictionId = gameInformation.AgeRestrictionId;
                gameInfo.Multiplayer = gameInformation.Multiplayer;
                gameInfo.Description = gameInformation.Description;
                gameInfo.Image = gameInformation.Image;
                gameInfo.YearPublished = gameInformation.YearPublished;

                _context.SaveChanges();
                return gameInfo;
            }

            throw new InvalidOperationException($"Game with ID {gameInformation.Id} not found.");
        }

        public List<GameInformation> GetAllGames()
        {
           return _context.GameInformations.Include(g => g.GameType).Include(g => g.Genre).Include(g => g.AgeRestriction).ToList();
        }

        public List<GameType> GetGameTypes()
        {
            return _context.GameTypes.OrderBy(gt => gt.Name).ToList();
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.OrderBy(g => g.Name).ToList();
        }

        public List<AgeRestriction> GetAgeRestrictions()
        {
            return _context.AgeRestrictions.OrderBy(ar => ar.Code).ToList();
        }

        public GameInformation GetGame(int id)
        {
           GameInformation? game = _context.GameInformations.Include(g => g.GameType).Include(g => g.Genre).Include(g => g.AgeRestriction).Where(x => x.Id == id).FirstOrDefault();
           return game ?? throw new InvalidOperationException($"Game with ID {id} not found.");
        }
    }
}
