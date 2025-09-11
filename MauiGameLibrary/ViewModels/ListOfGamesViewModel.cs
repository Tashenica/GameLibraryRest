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

        public async Task RefreshGamesAsync()
        {
            var games = await _gameDataService.GetAllGameInformation();
            
            // Load images for each game in parallel for better performance
            var imageLoadTasks = games.Select(LoadGameImage).ToArray();
            await Task.WhenAll(imageLoadTasks);
            
            Games = new ObservableCollection<GameInformation>(games);
        }

        public async void RefreshGames()
        {
            await RefreshGamesAsync();
        }

        private async Task LoadGameImage(GameInformation game)
        {
            try
            {
                if (game.Id > 0)
                {
                    var imageData = await _gameDataService.GetGameImage(game.Id);
                    if (imageData != null && imageData.Length > 0)
                    {
                        game.ImageData = imageData;
                        
                        // Create a temporary local file for display
                        var fileName = game.ImageFileName ?? $"game_image_{game.Id}.jpg";
                        var localPath = await SaveImageDataLocally(imageData, fileName);
                        if (!string.IsNullOrEmpty(localPath))
                        {
                            game.Image = localPath;
                        }
                    }
                    else
                    {
                        // No image in database, use default or empty
                        game.Image = "";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading image for game {game.Id}: {ex.Message}");
                game.Image = ""; // Fallback to no image
            }
        }

        private async Task<string> SaveImageDataLocally(byte[] imageData, string fileName)
        {
            try
            {
                var localAppData = FileSystem.AppDataDirectory;
                var imagesFolder = Path.Combine(localAppData, "Images");
                
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);
                
                var localFilePath = Path.Combine(imagesFolder, fileName);
                
                await File.WriteAllBytesAsync(localFilePath, imageData);
                
                return localFilePath;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving image data locally: {ex.Message}");
                return string.Empty;
            }
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            SelectedGame = null;
            RefreshGames();
        }
    }
}
