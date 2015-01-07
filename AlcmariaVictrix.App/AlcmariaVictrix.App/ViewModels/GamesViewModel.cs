using AlcmariaVictrix.Shared.Models;
using AlcmariaVictrix.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMolen.Mobile.Core.Interfaces;
using WebMolen.Mobile.Core.ViewModels;

namespace AlcmariaVictrix.Shared.ViewModels
{
    class GamesViewModel : ViewModelBase
    {
        private IEnumerable<GameViewModel> _areas;
        private readonly IGameService _mountainWeatherService;
        private readonly Func<Game, GameViewModel> _areaViewModelFactory;
        private readonly IDialogProvider _dialogProvider;

        public GamesViewModel(
            IGameService mountainWeatherService,
            Func<Game, GameViewModel> areaViewModelFactory,
            IDialogProvider dialogProvider)
        {
            this._dialogProvider = dialogProvider;
            _areaViewModelFactory = areaViewModelFactory;
            _mountainWeatherService = mountainWeatherService;
            Title = "Mountain Areas";
            SetAreas();
        }

        public IEnumerable<GameViewModel> Areas
        {
            get  { return _areas; }
            set  { SetProperty(ref _areas, value); }
        }

        private async void SetAreas()
        {
            try
            {
                IsBusy = true;
                var locations = await _mountainWeatherService.GetGames();

                if (locations == null)
                    return;

                Areas = locations
                    .Select(location =>  _areaViewModelFactory(location))
                    .ToList();
            }
            catch (Exception ex)
            {
                Action action = async () =>
                {
                    var result = await _dialogProvider.DisplayActionSheet(ex.Message, "Cancel", null, "Retry");

                    if (result == "Retry")
                        SetAreas();
                };

                action();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
