using AlcmariaVictrix.Shared.Models;
using AlcmariaVictrix.Shared.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebMolen.Mobile.Core.Services;
using WebMolen.Mobile.Core.ViewModels;
using Xamarin.Forms;

namespace AlcmariaVictrix.Shared.ViewModels
{
    public class CompetitionViewModel : ViewModelBase 
    {
        private readonly IGameService _gameService;
        private readonly INavigator _navigator;
        private readonly Competition _competition;

        public string NameSort
        {
            get
            {
                if (SportId == 1)
                    return "Honkbal";
                else if (SportId == 2)
                    return "Voetbal";
                else if (SportId == 3)
                    return "Softbal";
                else return "?";
                //if (string.IsNullOrWhiteSpace(Name) || Name.Length == 0)
                //    return "?";

                //return Name[0].ToString().ToUpper();
            }
        }

        public CompetitionViewModel(
            Competition competition, 
            IGameService gameService,
            INavigator navigator)
        {
            _competition = competition;
            _gameService = gameService;
            _navigator = navigator;

            ShowCompetitionCommand = new Command(ShowCompetition);
        }

        public string Name { get { return _competition.Team.Name; } }
        public int SportId { get { return _competition.Team.SportId; } }

        public ICommand ShowCompetitionCommand { get; set; }

        private async void ShowCompetition()
        {
            Debug.WriteLine("test");
            //await _navigator.PushAsync<ForecastReportViewModel>(_forecastReportViewModel);
            Competition competition = await _gameService.GetCompetitionInfo(_competition.Id);

            await _navigator.PushAsync<CompetitionInfoViewModel>(viewModel =>
            {
                viewModel.Title = competition.Name;
                viewModel.Name = competition.Name;
                viewModel.TeamName = competition.Team.Name;
                viewModel.Competition = competition;
                viewModel.Result = new ObservableCollection<ResultViewModel>(competition.Results.Select(r => new ResultViewModel(r.HomeTeam, r.AwayTeam, r.HomeScore, r.AwayScore)));
                viewModel.Games = new ObservableCollection<Game>(competition.Games);
            });
        }
    }
}
