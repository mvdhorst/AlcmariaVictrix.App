using AlcmariaVictrix.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcmariaVictrix.Shared.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetGames();

        Task<IEnumerable<Competition>> GetCompetitions();

        Task<Game> GetGame(int id);

        Task<Competition> GetCompetitionInfo(int id);

        Task<IEnumerable<FeedItem>> GetNewsItems();
    }
}
