using MauiGameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MauiGameLibrary.Interfaces;
using System.Text.Json;
using System.IO;

namespace MauiGameLibrary.Services
{
    public class GameDataService : IGameService
    {
        private List<GameInformation> _gameInformation = new List<GameInformation>();
        private List<GameType> _gameTypes = new List<GameType>();
        private List<AgeRestriction> _ageRestrictions = new List<AgeRestriction>();
        private List<Genre> _genres = new List<Genre>();

        public GameDataService()
        {
            LoadData();
            PrePopulateData();
            CreateFakeGameInformation();
        }

        public void PrePopulateData()
        {
            PrePopulateGameTypes();
            PrePopulateAgeRestrictions();
            PrePopulateGenres();
        }


        public void PrePopulateGameTypes()
        {
            GameType gameType = new GameType() { Id = 1, Name = "Nintendo 64", Description = "Nintendo's 64 bit console" };
            _gameTypes.Add(gameType);

            gameType = new GameType() { Id = 2, Name = "Nintendo Wii", Description = "Nintendo's family motion console." };
            _gameTypes.Add(gameType);

            gameType = new GameType() { Id = 3, Name = "Nintendo Switch", Description = "Nintendo's handheld console." };
            _gameTypes.Add(gameType);

            gameType = new GameType() { Id = 4, Name = "Playstation 5", Description = "Sony's latest console." };
            _gameTypes.Add(gameType);
        }

        public void PrePopulateAgeRestrictions()
        {
            AgeRestriction ageRestriction = new AgeRestriction() { Id = 1, Code = "E", Description = "Everyone - Content suitable for all ages" };
            _ageRestrictions.Add(ageRestriction);

            ageRestriction = new AgeRestriction() { Id = 2, Code = "E10+", Description = "Everyone 10+ - Content suitable for ages 10 and older" };
            _ageRestrictions.Add(ageRestriction);

            ageRestriction = new AgeRestriction() { Id = 3, Code = "T", Description = "Teen - Content suitable for ages 13 and older" };
            _ageRestrictions.Add(ageRestriction);

            ageRestriction = new AgeRestriction() { Id = 4, Code = "M", Description = "Mature 17+ - Content suitable for ages 17 and older" };
            _ageRestrictions.Add(ageRestriction);

            ageRestriction = new AgeRestriction() { Id = 5, Code = "AO", Description = "Adults Only 18+ - Content suitable only for adults" };
            _ageRestrictions.Add(ageRestriction);

            ageRestriction = new AgeRestriction() { Id = 6, Code = "RP", Description = "Rating Pending - Not yet assigned a final rating" };
            _ageRestrictions.Add(ageRestriction);
        }

        public void PrePopulateGenres()
        {
            Genre genre = new Genre() { Id = 1, Name = "Action", Description = "Fast-paced games with emphasis on physical challenges" };
            _genres.Add(genre);

            genre = new Genre() { Id = 2, Name = "Adventure", Description = "Story-driven games with exploration and puzzle-solving" };
            _genres.Add(genre);

            genre = new Genre() { Id = 3, Name = "RPG", Description = "Role-playing games with character development and story" };
            _genres.Add(genre);

            genre = new Genre() { Id = 4, Name = "Strategy", Description = "Games requiring tactical thinking and planning" };
            _genres.Add(genre);

            genre = new Genre() { Id = 5, Name = "Sports", Description = "Games simulating real-world sports activities" };
            _genres.Add(genre);

            genre = new Genre() { Id = 6, Name = "Racing", Description = "Vehicle racing and driving simulation games" };
            _genres.Add(genre);

            genre = new Genre() { Id = 7, Name = "Puzzle", Description = "Games focused on logic problems and brain teasers" };
            _genres.Add(genre);

            genre = new Genre() { Id = 8, Name = "Platformer", Description = "Games involving jumping between platforms and obstacles" };
            _genres.Add(genre);

            genre = new Genre() { Id = 9, Name = "Fighting", Description = "Combat-focused games with hand-to-hand or weapon combat" };
            _genres.Add(genre);

            genre = new Genre() { Id = 10, Name = "Simulation", Description = "Games that simulate real-world activities or systems" };
            _genres.Add(genre);
        }

        public Task<List<GameInformation>> GetAllGameInformation()
        {
            // if we wanted to not keep referenced objects automatically updated return _gameInformation.Select(gameInfo => (GameInformation)gameInfo.Clone()).ToList();
            return Task.FromResult(_gameInformation);
        }

        public void CreateFakeGameInformation()
        {
            // Wait for prepopulated data to be ready
            var wiiGameType = _gameTypes.FirstOrDefault(gt => gt.Name == "Nintendo Wii");
            var switchGameType = _gameTypes.FirstOrDefault(gt => gt.Name == "Nintendo Switch");
            var adventureGenre = _genres.FirstOrDefault(g => g.Name == "Adventure");
            var platformerGenre = _genres.FirstOrDefault(g => g.Name == "Platformer");
            var e10PlusRating = _ageRestrictions.FirstOrDefault(ar => ar.Code == "E10+");
            var everyoneRating = _ageRestrictions.FirstOrDefault(ar => ar.Code == "E");

            _gameInformation.Add(new GameInformation
            {
                Id = 1,
                Title = "The Legend of Zelda: Breath of the Wild",
                GameType = wiiGameType,
                CompanyName = "Nintendo",
                Genre = adventureGenre,
                AgeRestriction = e10PlusRating,
                Multiplayer = false,
                Description = "An open-world action-adventure game set in the kingdom of Hyrule.",
                Image = "zelda.png",
                YearPublished = new DateTime(2017, 3, 3)
            });
            _gameInformation.Add(new GameInformation
            {
                Id = 2,
                Title = "Super Mario Odyssey",
                GameType = switchGameType,
                CompanyName = "Nintendo",
                Genre = platformerGenre,
                AgeRestriction = everyoneRating,
                Multiplayer = false,
                Description = "A 3D platformer where Mario travels across various kingdoms to rescue Princess Peach.",
                Image = "mario.png",
                YearPublished = new DateTime(2017, 10, 27)
            });
        }

        public Task UpdateGameInformation(GameInformation gameInformation)
        {
            if (gameInformation.Id > 0)
            {
                //  UPDATE
                var uniqueGame = GetGameInformationById(gameInformation.Id).Result;

                int position = _gameInformation.IndexOf(uniqueGame);

                _gameInformation[position] = gameInformation;
            }
            else
            {
                // INSERT
                int id = _gameInformation.Count > 0 ? _gameInformation.Max(g => g.Id) + 1 : 1;
                gameInformation.Id = id;
                _gameInformation.Add(gameInformation);
            }

            SaveData();
            return Task.CompletedTask;
        }

        public Task<GameInformation> GetGameInformationById(int id)
        {
            var uniqueGame = _gameInformation.Where(gameInfo => gameInfo.Id == id).FirstOrDefault();

            return Task.FromResult(uniqueGame ?? new GameInformation());
        }

        public List<GameInformation> GetGameInformationByTitle(string title)
        {
            return _gameInformation.Where(gameInfo => gameInfo.Title == title).ToList();
        }


        public Task<List<GameType>> GetGameTypes()
        {
            return Task.FromResult(_gameTypes);
        }

        public Task<List<AgeRestriction>> GetAgeRestrictions()
        {
            return Task.FromResult(_ageRestrictions);
        }

        public Task<List<Genre>> GetGenres()
        {
            return Task.FromResult(_genres);
        }

        private string GetStoragePath()
        {
          string folderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
              "gamedata.json");

            return folderPath;
        }


        public void SaveData()
        {
           string jsonResult = JsonSerializer.Serialize(_gameInformation);
           string path = GetStoragePath();

           File.WriteAllText(path, jsonResult);

        }

        public void LoadData()
        {
            string path = GetStoragePath();

            if (File.Exists(path))
            {
                string jsonResult = File.ReadAllText(path);
                _gameInformation = JsonSerializer.Deserialize<List<GameInformation>>(jsonResult) ?? new List<GameInformation>();
            }        
        
        }

        public Task<byte[]?> GetGameImage(int gameId)
        {
            var game = _gameInformation.FirstOrDefault(g => g.Id == gameId);
            return Task.FromResult(game?.ImageData);
        }

        public Task<bool> UploadGameImage(int gameId, byte[] imageData, string fileName, string contentType)
        {
            var game = _gameInformation.FirstOrDefault(g => g.Id == gameId);
            if (game != null)
            {
                game.ImageData = imageData;
                game.ImageFileName = fileName;
                game.ImageContentType = contentType;
                SaveData();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> DeleteGameImage(int gameId)
        {
            var game = _gameInformation.FirstOrDefault(g => g.Id == gameId);
            if (game != null)
            {
                game.ImageData = null;
                game.ImageFileName = null;
                game.ImageContentType = null;
                SaveData();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<GameInformation> CreateGameInformation(GameInformation gameInformation)
        {
            // Generate a new ID for the game
            gameInformation.Id = _gameInformation.Count > 0 ? _gameInformation.Max(g => g.Id) + 1 : 1;
            
            _gameInformation.Add(gameInformation);
            SaveData();
            
            return Task.FromResult(gameInformation);
        }
    }
}

