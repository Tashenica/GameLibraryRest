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
            return _games.Where(x => x.Id == id).FirstOrDefault();
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

        private void CreateFakeGameInformation()
        {
            _games.Add(new GameInformation
            {
                Id = 1,
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
            _games.Add(new GameInformation
            {
                Id = 2,
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
    }
}
