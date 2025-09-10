using MauiGameLibrary.Interfaces;
using MauiGameLibrary.Models;
using MauiGameLibrary.Services;
using System.Windows.Input;

namespace MauiGameLibrary.ViewModels
{
    [QueryProperty(nameof(SelectedGame), nameof(SelectedGame))]
    public class UpdateGameViewModel : BaseViewModel
    {
        private IGameService _gameDataServices;

        private GameInformation _selectedGame = new GameInformation();

        public GameInformation SelectedGame
        {
            get { return _selectedGame; }
            set
            {
                _selectedGame = value;
                OnPropertyChanged();
                
                UpdateSelectedGameType();
                UpdateSelectedAgeRestriction();
                UpdateSelectedGenre();
            }
        }

        private List<GameType> _gameTypes = new List<GameType>();

        public List<GameType> GameTypes
        {
            get { return _gameTypes; }
            set
            {
                _gameTypes = value;
                OnPropertyChanged();
            }
        }

        private GameType? _selectedGameType;

        public GameType? SelectedGameType
        {
            get { return _selectedGameType; }
            set
            {
                _selectedGameType = value;
                OnPropertyChanged();
                
                if (_selectedGameType != null && SelectedGame != null)
                {
                    SelectedGame.GameType = _selectedGameType.Name;
                    OnPropertyChanged(nameof(SelectedGame));
                }
            }
        }

        private List<AgeRestriction> _ageRestrictions = new List<AgeRestriction>();

        public List<AgeRestriction> AgeRestrictions
        {
            get { return _ageRestrictions; }
            set
            {
                _ageRestrictions = value;
                OnPropertyChanged();
            }
        }

        private AgeRestriction? _selectedAgeRestriction;

        public AgeRestriction? SelectedAgeRestriction
        {
            get { return _selectedAgeRestriction; }
            set
            {
                _selectedAgeRestriction = value;
                OnPropertyChanged();
                
                if (_selectedAgeRestriction != null && SelectedGame != null)
                {
                    SelectedGame.AgeRestriction = _selectedAgeRestriction.Code;
                    OnPropertyChanged(nameof(SelectedGame));
                }
            }
        }

        private List<Genre> _genres = new List<Genre>();

        public List<Genre> Genres
        {
            get { return _genres; }
            set
            {
                _genres = value;
                OnPropertyChanged();
            }
        }

        private Genre? _selectedGenre;

        public Genre? SelectedGenre
        {
            get { return _selectedGenre; }
            set
            {
                _selectedGenre = value;
                OnPropertyChanged();
                
                if (_selectedGenre != null && SelectedGame != null)
                {
                    SelectedGame.Genre = _selectedGenre.Name;
                    OnPropertyChanged(nameof(SelectedGame));
                }
            }
        }

        public ICommand SaveChangesCommand { get; }
        public ICommand SelectImageCommand { get; }

       
        public UpdateGameViewModel(IGameService gameDataServices)
        {
            _gameDataServices = gameDataServices;
            SaveChangesCommand = new Command(async () => await SaveChanges());
            SelectImageCommand = new Command(async () => await SelectImage());
        }

        private async Task SaveChanges()
        {
            try
            {
                if (SelectedGame != null && IsValidGame())
                {
                    _gameDataServices.UpdateGameInformation(SelectedGame);
                    
                    // Navigate back to the previous page
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Cannot save: Required fields are missing");
                    // In a production app, you might want to show a user-friendly message
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        private bool IsValidGame()
        {
            if (SelectedGame == null) return false;

            // Check all required fields
            return !string.IsNullOrWhiteSpace(SelectedGame.Title) &&
                   !string.IsNullOrWhiteSpace(SelectedGame.CompanyName) &&
                   !string.IsNullOrWhiteSpace(SelectedGame.Description) &&
                   SelectedGameType != null &&
                   SelectedGenre != null &&
                   SelectedAgeRestriction != null;
        }

        private async Task SelectImage()
        {
            try
            {
                if (!MediaPicker.Default.IsCaptureSupported)
                {
                    System.Diagnostics.Debug.WriteLine("Media picker not supported on this device");
                    return;
                }

                var result = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select a game image"
                });

                if (result != null)
                {
                    var localImagePath = await SaveImageLocally(result);
                    
                    if (!string.IsNullOrEmpty(localImagePath))
                    {
                        SelectedGame.Image = localImagePath;
                        OnPropertyChanged(nameof(SelectedGame));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error selecting image: {ex.Message}");
            }
        }

        private async Task<string> SaveImageLocally(FileResult fileResult)
        {
            try
            {
                var fileName = $"game_image_{Guid.NewGuid()}{Path.GetExtension(fileResult.FileName)}";
                
                var localAppData = FileSystem.AppDataDirectory;
                var imagesFolder = Path.Combine(localAppData, "Images");
                
                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);
                
                var localFilePath = Path.Combine(imagesFolder, fileName);
                
                using var sourceStream = await fileResult.OpenReadAsync();
                using var localFileStream = File.Create(localFilePath);
                await sourceStream.CopyToAsync(localFileStream);
                
                return localFilePath;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving image locally: {ex.Message}");
                return string.Empty;
            }
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            GameTypes = _gameDataServices.GetGameTypes();
            AgeRestrictions = _gameDataServices.GetAgeRestrictions();
            Genres = _gameDataServices.GetGenres();

            if (string.IsNullOrEmpty(SelectedGame.Id))
            {
                SelectedGame.GameType = GameTypes.First().Name;
                SelectedGame.AgeRestriction = AgeRestrictions.First().Code;
                SelectedGame.Genre = Genres.First().Name;
            }

            UpdateSelectedGameType();
            UpdateSelectedAgeRestriction();
            UpdateSelectedGenre();
        }

        private void UpdateSelectedGameType()
        {
            if (SelectedGame != null && GameTypes != null && !string.IsNullOrEmpty(SelectedGame.GameType))
            {
                SelectedGameType = GameTypes.FirstOrDefault(gt => gt.Name == SelectedGame.GameType);
            }
        }

        private void UpdateSelectedAgeRestriction()
        {
            if (SelectedGame != null && AgeRestrictions != null && !string.IsNullOrEmpty(SelectedGame.AgeRestriction))
            {
                SelectedAgeRestriction = AgeRestrictions.FirstOrDefault(ar => ar.Code == SelectedGame.AgeRestriction);
            }
        }

        private void UpdateSelectedGenre()
        {
            if (SelectedGame != null && Genres != null && !string.IsNullOrEmpty(SelectedGame.Genre))
            {
                SelectedGenre = Genres.FirstOrDefault(g => g.Name == SelectedGame.Genre);
            }
        }
    }
}
