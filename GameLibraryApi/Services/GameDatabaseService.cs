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
            gameInformation.GameType = null;
            gameInformation.Genre = null;
            gameInformation.AgeRestriction = null;
            
            _context.GameInformations.Add(gameInformation);
            _context.SaveChanges();

            return GetGame(gameInformation.Id) ?? gameInformation;
        }

        public void DeleteGame(int id)
        {
            GameInformation? gameInfo = GetGame(id);
            if (gameInfo != null)
            {
                _context.Remove(gameInfo);
                _context.SaveChanges();
            }
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
                gameInfo.ImageData = gameInformation.ImageData;
                gameInfo.ImageFileName = gameInformation.ImageFileName;
                gameInfo.ImageContentType = gameInformation.ImageContentType;
                gameInfo.YearPublished = gameInformation.YearPublished;

                _context.SaveChanges();
                return gameInfo;
            }

            return gameInformation;
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

        public GameInformation? GetGame(int id)
        {
           GameInformation? game = _context.GameInformations.Include(g => g.GameType).Include(g => g.Genre).Include(g => g.AgeRestriction).Where(x => x.Id == id).FirstOrDefault();
           return game;
        }
    }
}
