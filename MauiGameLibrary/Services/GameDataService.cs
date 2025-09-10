using MauiGameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using MauiGameLibrary.Interfaces;

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
            CreateFakeGameInformation();
            LoadData();
            PrePopulateData();
        }

        public void PrePopulateData()
        {
            PrePopulateGameTypes();
            PrePopulateAgeRestrictions();
            PrePopulateGenres();
        }


        public void PrePopulateGameTypes()
        {
            GameType gameType = new GameType() { Name = "Nintendo 64", Description = "Nintendo's 64 bit console" };
            _gameTypes.Add(gameType);

            gameType = new GameType() { Name = "Nintendo Wii", Description = "Nintendo's family motion console." };
            _gameTypes.Add(gameType);

            gameType = new GameType() { Name = "Nintendo Switch", Description = "Nintendo's handheld console." };
            _gameTypes.Add(gameType);

            gameType = new GameType() { Name = "Playstation 5", Description = "Sony's latest console." };
            _gameTypes.Add(gameType);
        }

        public void PrePopulateAgeRestrictions()
        {
            AgeRestriction ageRestriction = new AgeRestriction() { Code = "E", Description = "Everyone - Content suitable for all ages" };
            _ageRestrictions.Add(ageRestriction);

            ageRestriction = new AgeRestriction() { Code = "E10+", Description = "Everyone 10+ - Content suitable for ages 10 and older" };
            _ageRestrictions.Add(ageRestriction);

            ageRestriction = new AgeRestriction() { Code = "T", Description = "Teen - Content suitable for ages 13 and older" };
            _ageRestrictions.Add(ageRestriction);

            ageRestriction = new AgeRestriction() { Code = "M", Description = "Mature 17+ - Content suitable for ages 17 and older" };
            _ageRestrictions.Add(ageRestriction);

            ageRestriction = new AgeRestriction() { Code = "AO", Description = "Adults Only 18+ - Content suitable only for adults" };
            _ageRestrictions.Add(ageRestriction);

            ageRestriction = new AgeRestriction() { Code = "RP", Description = "Rating Pending - Not yet assigned a final rating" };
            _ageRestrictions.Add(ageRestriction);
        }

        public void PrePopulateGenres()
        {
            Genre genre = new Genre() { Name = "Action", Description = "Fast-paced games with emphasis on physical challenges" };
            _genres.Add(genre);

            genre = new Genre() { Name = "Adventure", Description = "Story-driven games with exploration and puzzle-solving" };
            _genres.Add(genre);

            genre = new Genre() { Name = "RPG", Description = "Role-playing games with character development and story" };
            _genres.Add(genre);

            genre = new Genre() { Name = "Strategy", Description = "Games requiring tactical thinking and planning" };
            _genres.Add(genre);

            genre = new Genre() { Name = "Sports", Description = "Games simulating real-world sports activities" };
            _genres.Add(genre);

            genre = new Genre() { Name = "Racing", Description = "Vehicle racing and driving simulation games" };
            _genres.Add(genre);

            genre = new Genre() { Name = "Puzzle", Description = "Games focused on logic problems and brain teasers" };
            _genres.Add(genre);

            genre = new Genre() { Name = "Platformer", Description = "Games involving jumping between platforms and obstacles" };
            _genres.Add(genre);

            genre = new Genre() { Name = "Fighting", Description = "Combat-focused games with hand-to-hand or weapon combat" };
            _genres.Add(genre);

            genre = new Genre() { Name = "Simulation", Description = "Games that simulate real-world activities or systems" };
            _genres.Add(genre);
        }

        public List<GameInformation> GetAllGameInformation()
        {
            // if we wanted to not keep referenced objects automatically updated return _gameInformation.Select(gameInfo => (GameInformation)gameInfo.Clone()).ToList();
            return _gameInformation;
        }

        public void CreateFakeGameInformation()
        {
            _gameInformation.Add(new GameInformation
            {
                Id = Guid.NewGuid().ToString(),
                Title = "The Legend of Zelda: Breath of the Wild",
                GameType = "Nintendo Wii",
                CompanyName = "Nintendo",
                Genre = "Adventure",
                AgeRestriction = "E10+",
                Multiplayer = false,
                Description = "An open-world action-adventure game set in the kingdom of Hyrule.",
                Image = "zelda.png",
                YearPublished = new DateTime(2017, 3, 3)
            });
            _gameInformation.Add(new GameInformation
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Super Mario Odyssey",
                GameType = "Nintendo Switch",
                CompanyName = "Nintendo",
                Genre = "Platformer",
                AgeRestriction = "E",
                Multiplayer = false,
                Description = "A 3D platformer where Mario travels across various kingdoms to rescue Princess Peach.",
                Image = "mario.png",
                YearPublished = new DateTime(2017, 10, 27)
            });
        }

        public void UpdateGameInformation(GameInformation gameInformation)
        {
            if (!string.IsNullOrEmpty(gameInformation.Id))
            {
                //  UPDATE
                var uniqueGame = GetGameInformationById(gameInformation.Id);

                int position = _gameInformation.IndexOf(uniqueGame);

                _gameInformation[position] = gameInformation;
            }
            else
            {
                // INSERT
                string id = Guid.NewGuid().ToString();
                gameInformation.Id = id;
                _gameInformation.Add(gameInformation);
            }

            SaveData();
        }

        public GameInformation GetGameInformationById(string id)
        {
            var uniqueGame = _gameInformation.Where(gameInfo => gameInfo.Id == id).FirstOrDefault();

            return uniqueGame;
        }

        public List<GameInformation> GetGameInformationByTitle(string title)
        {
            return _gameInformation.Where(gameInfo => gameInfo.Title == title).ToList();
        }


        public List<GameType> GetGameTypes()
        {
            return _gameTypes;
        }

        public List<AgeRestriction> GetAgeRestrictions()
        {
            return _ageRestrictions;
        }

        public List<Genre> GetGenres()
        {
            return _genres;
        }

        private string GetStoragePath()
        {
          string folderPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
              "gamedata.json");

            return folderPath;
        }


        public void SaveData()
        {
           string jsonResult = JsonConvert.SerializeObject(_gameInformation);
           string path = GetStoragePath();

           File.WriteAllText(path, jsonResult);

        }

        public void LoadData()
        {
            string path = GetStoragePath();

            if (File.Exists(path))
            {
                string jsonResult = File.ReadAllText(path);
                _gameInformation = JsonConvert.DeserializeObject<List<GameInformation>>(jsonResult);
            }        
        
        }

        Task<List<GameInformation>> IGameService.GetAllGameInformation()
        {
            throw new NotImplementedException();
        }
    }
}

