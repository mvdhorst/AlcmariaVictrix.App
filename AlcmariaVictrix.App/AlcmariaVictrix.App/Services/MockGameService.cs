using AlcmariaVictrix.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcmariaVictrix.Shared.Services
{
    public class MockGameService : IGameService
    {

        public Task<List<Game>> GetGames()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Competition>> GetCompetitions()
        {
            throw new NotImplementedException();
        }

        public Task<Models.Game> GetGame(int id)
        {
            throw new NotImplementedException();
        }


        public Task<Models.Competition> GetCompetitionInfo(int id)
        {
            throw new NotImplementedException();
        }
    }
}
