using MauiGameLibrary.Configuration;
using MauiGameLibrary.Exceptions;
using MauiGameLibrary.Interfaces;
using MauiGameLibrary.Models;
using Newtonsoft.Json;
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
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
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
        public List<AgeRestriction> GetAgeRestrictions()
        {
            throw new NotImplementedException();
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

                    List<GameInformation> games = JsonConvert.DeserializeObject<List<GameInformation>>(content);

                    return games;
                }

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new GameApiFailedException("Failed to fetch game data from API.");
            }

            return null;
        }

        public GameInformation GetGameInformationById(string id)
        {
            throw new NotImplementedException();
        }

        public List<GameType> GetGameTypes()
        {
            throw new NotImplementedException();
        }

        public List<Genre> GetGenres()
        {
            throw new NotImplementedException();
        }

        public void UpdateGameInformation(GameInformation gameInformation)
        {
            throw new NotImplementedException();
        }


    }
}
