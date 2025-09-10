using GameLibraryApi.Interfaces;
using GameLibraryApi.Models;

namespace GameLibraryApi.Services
{
    public class GameService : IGameService
    {
        private List<GameInformation> _games;

        public GameService()
        {
            _games = new List<GameInformation>();

            CreateFakeGameInformation();
        }

        public List<GameInformation> GetAllGames()
        {
            return _games;
        }

        public GameInformation GetGame(int id)
        {
            GameInformation? game = _games.Where(x => x.Id == id).FirstOrDefault();
            return game ?? throw new InvalidOperationException($"Game with ID {id} not found.");
        }

        public GameInformation CreateGame(GameInformation gameInformation)
        {
            _games.Add(gameInformation);

            return GetGame(gameInformation.Id); ;

        }

        public GameInformation EditGame(GameInformation gameInformation)
        {
           
            GameInformation gameInformationToEdit = GetGame(gameInformation.Id);
            
            int pos = _games.IndexOf(gameInformationToEdit);
            _games[pos] = gameInformation;

            return gameInformation;
        }

        public void DeleteGame(int id)
        {
            GameInformation gameInformationToDelete = GetGame(id);
            _games.Remove(gameInformationToDelete);
        }

        public List<GameType> GetGameTypes()
        {
            // For the in-memory service, return hardcoded GameType objects
            return new List<GameType>
            {
                new GameType { Id = 1, Name = "Nintendo Wii", Description = "Games for Nintendo Wii console" },
                new GameType { Id = 2, Name = "Nintendo Switch", Description = "Games for Nintendo Switch console" },
                new GameType { Id = 3, Name = "PlayStation 5", Description = "Games for Sony PlayStation 5 console" },
                new GameType { Id = 4, Name = "Xbox Series X", Description = "Games for Microsoft Xbox Series X console" },
                new GameType { Id = 5, Name = "PC", Description = "Games for Personal Computer" }
            };
        }

        public List<Genre> GetGenres()
        {
            // For the in-memory service, return hardcoded Genre objects
            return new List<Genre>
            {
                new Genre { Id = 1, Name = "Adventure", Description = "Adventure and exploration games" },
                new Genre { Id = 2, Name = "Platformer", Description = "Platform jumping games" },
                new Genre { Id = 3, Name = "Action", Description = "Fast-paced action games" },
                new Genre { Id = 4, Name = "RPG", Description = "Role-playing games" },
                new Genre { Id = 5, Name = "Strategy", Description = "Strategic thinking games" },
                new Genre { Id = 6, Name = "Sports", Description = "Sports simulation games" },
                new Genre { Id = 7, Name = "Racing", Description = "Car and bike racing games" },
                new Genre { Id = 8, Name = "Shooter", Description = "First and third person shooter games" }
            };
        }

        public List<AgeRestriction> GetAgeRestrictions()
        {
            // For the in-memory service, return hardcoded AgeRestriction objects
            return new List<AgeRestriction>
            {
                new AgeRestriction { Id = 1, Code = "E", Description = "Everyone - Content suitable for all ages" },
                new AgeRestriction { Id = 2, Code = "E10+", Description = "Everyone 10+ - Content suitable for ages 10 and older" },
                new AgeRestriction { Id = 3, Code = "T", Description = "Teen - Content suitable for ages 13 and older" },
                new AgeRestriction { Id = 4, Code = "M", Description = "Mature 17+ - Content suitable for ages 17 and older" },
                new AgeRestriction { Id = 5, Code = "AO", Description = "Adults Only 18+ - Content suitable only for adults" },
                new AgeRestriction { Id = 6, Code = "RP", Description = "Rating Pending - Not yet assigned a final rating" }
            };
        }

        private void CreateFakeGameInformation()
        {
            _games.Add(new GameInformation
            {
                Id = 1,
                Title = "The Legend of Zelda: Breath of the Wild",
                GameTypeId = 1, // Nintendo Wii
                GenreId = 1, // Adventure
                AgeRestrictionId = 2, // E10+
                CompanyName = "Nintendo",
                Multiplayer = false,
                Description = "An open-world action-adventure game set in the kingdom of Hyrule.",
                Image = "zelda.png",
                YearPublished = new DateTime(2017, 3, 3)
            });
            _games.Add(new GameInformation
            {
                Id = 2,
                Title = "Super Mario Odyssey",
                GameTypeId = 2, // Nintendo Switch
                GenreId = 2, // Platformer
                AgeRestrictionId = 1, // E
                CompanyName = "Nintendo",
                Multiplayer = false,
                Description = "A 3D platformer where Mario travels across various kingdoms to rescue Princess Peach.",
                Image = "mario.png",
                YearPublished = new DateTime(2017, 10, 27)
            });
        }
    }
}
