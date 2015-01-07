using AlcmariaVictrix.Shared.Models;
using AlcmariaVictrix.Shared.Services;
using System;
using System.Collections.Generic;
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

        public string Name { get { return _competition.Name; } }

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
            });
        }
    }
}
