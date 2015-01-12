using AlcmariaVictrix.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebMolen.Mobile.Core.Services;
using WebMolen.Mobile.Core.ViewModels;
using Xamarin.Forms;

namespace AlcmariaVictrix.Shared.ViewModels
{
    class GameViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private Game _game;
        private readonly GameViewModel _gameViewModel;

        public Game Game
        {
            get { return _game; }
            set { SetProperty(ref _game, value); }
        }

        public GameViewModel(
            Game game, 
            INavigator navigator)
            //Func<Game, ForecastReportViewModel> forecastReportViewModelFactory)
        {
            _game = game;
            _navigator = navigator;
            //_gameViewModel = ViewModelFactory
            //_forecastReportViewModel = forecastReportViewModelFactory(_location);

            ShowGameCommand = new Command(ShowGame);
        }

        public ICommand ShowGameCommand { get; set; }

        private async void ShowGame()
        {

            //await _navigator.PushAsync<ForecastReportViewModel>(_forecastReportViewModel);
        }
    }
}
