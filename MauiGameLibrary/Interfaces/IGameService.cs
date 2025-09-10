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
        List<AgeRestriction> GetAgeRestrictions();
        Task<List<GameInformation>> GetAllGameInformation();
        GameInformation GetGameInformationById(string id);
        List<GameType> GetGameTypes();
        List<Genre> GetGenres();
       /* void LoadData();
        void SaveData();*/
        void UpdateGameInformation(GameInformation gameInformation);
    }
}
