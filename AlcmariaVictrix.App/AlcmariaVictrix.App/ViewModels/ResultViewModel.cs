using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMolen.Mobile.Core.ViewModels;

namespace AlcmariaVictrix.Shared.ViewModels
{
    public class ResultViewModel : ViewModelBase 
    {
        public ResultViewModel()
        {

        }
        public ResultViewModel(string homeTeam, string awayTeam, string homeScore, string awayScore)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeScore = homeScore;
            AwayScore = awayScore;
        }
        public string HomeTeam {get; set;}
        public string AwayTeam {get;set;}
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
    }
}
