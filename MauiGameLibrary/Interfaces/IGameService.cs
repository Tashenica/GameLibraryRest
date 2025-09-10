using MauiGameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiGameLibrary.Interfaces
{
    public interface IGameService
    {
        Task<List<AgeRestriction>> GetAgeRestrictions();
        Task<List<GameInformation>> GetAllGameInformation();
        Task<GameInformation> GetGameInformationById(int id);
        Task<List<GameType>> GetGameTypes();
        Task<List<Genre>> GetGenres();
       /* void LoadData();
        void SaveData();*/
        Task UpdateGameInformation(GameInformation gameInformation);
    }
}
