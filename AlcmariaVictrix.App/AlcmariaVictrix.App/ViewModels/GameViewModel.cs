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
        private readonly Game _location;
        //private readonly ForecastReportViewModel _forecastReportViewModel;

        public GameViewModel(
            Game location, 
            INavigator navigator)
            //Func<Game, ForecastReportViewModel> forecastReportViewModelFactory)
        {
            _location = location;
            _navigator = navigator;
            //_forecastReportViewModel = forecastReportViewModelFactory(_location);

            ShowForecastCommand = new Command(ShowForecast);
        }

        public string Name { get { return _location.GameNumber; } }

        public DateTime IssuedDate { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public ICommand ShowForecastCommand { get; set; }

        private async void ShowForecast()
        {
            //await _navigator.PushAsync<ForecastReportViewModel>(_forecastReportViewModel);
        }
    }
}
