using MauiGameLibrary.Configuration;
using MauiGameLibrary.Exceptions;
using MauiGameLibrary.Interfaces;
using MauiGameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiGameLibrary.Services
{
    public class GameDataApiService : IGameService
    {
        private HttpClient _apiClient;
        private ApplicationSettings _applicationSettings;

        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, // Handles case mismatches between API and model
            WriteIndented = true
        };

        public GameDataApiService(ApplicationSettings applicationSettings)
        {
            

#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _apiClient = new HttpClient(insecureHandler);
#else
            _apiClient = new HttpClient();
#endif
            _applicationSettings = applicationSettings;
        }

        private HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
        public async Task<List<AgeRestriction>> GetAgeRestrictions()
        {
            Uri uri = new Uri($"{_applicationSettings.ServiceUrl}/agerestrictions");

            try
            {
                HttpResponseMessage response = await _apiClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // Deserialize directly to AgeRestriction objects since API now returns them
                    List<AgeRestriction>? ageRestrictions = JsonSerializer.Deserialize<List<AgeRestriction>>(content, _jsonSerializerOptions);

                    return ageRestrictions ?? new List<AgeRestriction>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new GameApiFailedException("Failed to fetch age restrictions from API.");
            }

            return new List<AgeRestriction>();
        }

        public async Task<List<GameInformation>> GetAllGameInformation()
        {
            Uri uri = new Uri(_applicationSettings.ServiceUrl);

            try
            {

                HttpResponseMessage response = await _apiClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    List<GameInformation>? games = JsonSerializer.Deserialize<List<GameInformation>>(content, _jsonSerializerOptions);


                    return games ?? new List<GameInformation>();
                }

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new GameApiFailedException("Failed to fetch game data from API.");
            }

            return new List<GameInformation>();
        }

        public async Task<GameInformation> GetGameInformationById(int id)
        {
            Uri uri = new Uri($"{_applicationSettings.ServiceUrl}/{id}");

            try
            {
                HttpResponseMessage response = await _apiClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    GameInformation? game = JsonSerializer.Deserialize<GameInformation>(content, _jsonSerializerOptions);

                    return game ?? new GameInformation();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new GameApiFailedException($"Failed to fetch game data for ID {id} from API.");
            }

            throw new GameApiFailedException($"Failed to fetch game data for ID {id} from API.");
        }

        public async Task<List<GameType>> GetGameTypes()
        {
            Uri uri = new Uri($"{_applicationSettings.ServiceUrl}/gametypes");

            try
            {
                HttpResponseMessage response = await _apiClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // Deserialize directly to GameType objects since API now returns them
                    List<GameType>? gameTypes = JsonSerializer.Deserialize<List<GameType>>(content, _jsonSerializerOptions);

                    return gameTypes ?? new List<GameType>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new GameApiFailedException("Failed to fetch game types from API.");
            }

            return new List<GameType>();
        }

        public async Task<List<Genre>> GetGenres()
        {
            Uri uri = new Uri($"{_applicationSettings.ServiceUrl}/genres");

            try
            {
                HttpResponseMessage response = await _apiClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    // Deserialize directly to Genre objects since API now returns them
                    List<Genre>? genres = JsonSerializer.Deserialize<List<Genre>>(content, _jsonSerializerOptions);

                    return genres ?? new List<Genre>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new GameApiFailedException("Failed to fetch genres from API.");
            }

            return new List<Genre>();
        }

        public async Task<GameInformation> CreateGameInformation(GameInformation gameInformation)
        {
            Uri uri = new Uri(_applicationSettings.ServiceUrl);

            try
            {
                string jsonContent = JsonSerializer.Serialize(gameInformation, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _apiClient.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    GameInformation? createdGame = JsonSerializer.Deserialize<GameInformation>(responseContent, _jsonSerializerOptions);
                    return createdGame ?? new GameInformation();
                }
                else
                {
                    Debug.WriteLine($"Create failed with status: {response.StatusCode}");
                    throw new GameApiFailedException($"Failed to create game. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new GameApiFailedException("Failed to create game data.");
            }
        }

        public async Task UpdateGameInformation(GameInformation gameInformation)
        {
            Uri uri = new Uri($"{_applicationSettings.ServiceUrl}/{gameInformation.Id}");

            try
            {
                string jsonContent = JsonSerializer.Serialize(gameInformation, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _apiClient.PutAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Update failed with status: {response.StatusCode}");
                    throw new GameApiFailedException($"Failed to update game with ID {gameInformation.Id}. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new GameApiFailedException($"Failed to update game data for ID {gameInformation.Id}.");
            }
        }

        public async Task<byte[]?> GetGameImage(int gameId)
        {
            Uri uri = new Uri($"{_applicationSettings.ServiceUrl}/{gameId}/image");

            try
            {
                HttpResponseMessage response = await _apiClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // No image found for this game
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new GameApiFailedException($"Failed to fetch image for game ID {gameId}.");
            }

            return null;
        }

        public async Task<bool> UploadGameImage(int gameId, byte[] imageData, string fileName, string contentType)
        {
            Uri uri = new Uri($"{_applicationSettings.ServiceUrl}/{gameId}/image");

            try
            {
                using var form = new MultipartFormDataContent();
                using var imageContent = new ByteArrayContent(imageData);
                imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                form.Add(imageContent, "image", fileName);

                HttpResponseMessage response = await _apiClient.PostAsync(uri, form);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Debug.WriteLine($"Image upload failed with status: {response.StatusCode}");
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Error content: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new GameApiFailedException($"Failed to upload image for game ID {gameId}.");
            }

            return false;
        }

        public async Task<bool> DeleteGameImage(int gameId)
        {
            Uri uri = new Uri($"{_applicationSettings.ServiceUrl}/{gameId}/image");

            try
            {
                HttpResponseMessage response = await _apiClient.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // No image found to delete
                    return true;
                }
                else
                {
                    Debug.WriteLine($"Image deletion failed with status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new GameApiFailedException($"Failed to delete image for game ID {gameId}.");
            }

            return false;
        }


    }
}
