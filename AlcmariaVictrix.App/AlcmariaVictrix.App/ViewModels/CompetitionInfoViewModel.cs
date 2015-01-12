using AlcmariaVictrix.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMolen.Mobile.Core.Helpers;
using WebMolen.Mobile.Core.ViewModels;

namespace AlcmariaVictrix.Shared.ViewModels
{
    public class CompetitionInfoViewModel : ViewModelBase
    {
        public CompetitionInfoViewModel()
        {

        }

        private string _competitionName;
        private string _teamName;
        private ObservableCollection<ResultViewModel> _result;
        private ObservableCollection<Game> _games;
        private ObservableCollection<Grouping<DateTime, Game>> _gamesGrouped;
        public Competition Competition { get; set; }
        public ObservableCollection<Grouping<DateTime, Game>> GamesGrouped
        {
            get { return _gamesGrouped; }
            set { SetProperty(ref _gamesGrouped, value); }
        }

        public ObservableCollection<ResultViewModel> Result { 
            get { return _result; } 
            set { SetProperty(ref _result, value); } }

        public ObservableCollection<Game> Games
        {
            get { return _games; }
            set 
            { 
                SetProperty(ref _games, value);
                foreach (Game game in _games)
                    game.Competition = Competition;
                GroupGames();
            }
        }

        private void GroupGames()
        {            
            //Use linq to sorty our monkeys by name and then group them by the new name sort property
            var sorted = from game in Games
                         orderby game.GameDate
                         group game by game.DateSort into gameGroup
                         select new Grouping<DateTime, Game>(gameGroup.Key, gameGroup);

            //create a new collection of groups
            GamesGrouped = new ObservableCollection<Grouping<DateTime, Game>>(sorted);
            //await GroupGamesAsync();
        }

        private Task GroupGamesAsync()
        {
            return new Task(() => { 

            });
   
        }

        public string Name { 
            get { return _competitionName; }
            set { SetProperty(ref _competitionName, value); } 
        }

        public string TeamName
        {
            get { return _teamName; }
            set { SetProperty(ref _teamName, value); }
        }
    }
}
