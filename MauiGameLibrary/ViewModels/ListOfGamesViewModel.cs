using MauiGameLibrary.Interfaces;
using MauiGameLibrary.Models;
using MauiGameLibrary.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiGameLibrary.ViewModels
{
    public class ListOfGamesViewModel : BaseViewModel
    {
        private IGameService _gameDataService;
        private ObservableCollection<GameInformation> _games = new ObservableCollection<GameInformation>();

        private GameInformation? _selectedGame;

        public GameInformation? SelectedGame
        {
            get { return _selectedGame; }
            set
            {
                _selectedGame = value;

                OnPropertyChanged(nameof(SelectedGame));
            }
        }


        public ICommand GameSelectedCommand { get; set; }
        public ICommand AddGameCommand { get; set; }

        public ObservableCollection<GameInformation> Games
        {
            get { return _games; }
            set
            {

                _games = value;
                OnPropertyChanged();
            }
        }

        public ListOfGamesViewModel(IGameService service)
        {
            _gameDataService = service;

            GameSelectedCommand = new Command(GameSelected);
            AddGameCommand = new Command(async () => await AddGame());
        }

        private async void GameSelected(object obj)
        {
            if (SelectedGame != null)
            {
                var param = new ShellNavigationQueryParameters()
            {
                { "SelectedGame", SelectedGame }
            };

                await AppShell.Current.GoToAsync("updategameroute", param);
            }
           
        }

        private async Task AddGame()
        {
            // Create a new empty game
            var newGame = new GameInformation
            {
                Id = 0, // Zero ID indicates a new game
                Title = "",
                GameType = null, // Will be set in the update view
                CompanyName = "",
                Genre = null, // Will be set in the update view
                AgeRestriction = null, // Will be set in the update view
                Multiplayer = false,
                Description = "",
                Image = "",
                YearPublished = DateTime.Now
            };

            var param = new ShellNavigationQueryParameters()
            {
                { "SelectedGame", newGame }
            };

            await AppShell.Current.GoToAsync("updategameroute", param);
        }

        public async void RefreshGames()
        {
            Games = new ObservableCollection<GameInformation>(await _gameDataService.GetAllGameInformation());
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            SelectedGame = null;
            RefreshGames();
        }
    }
}
