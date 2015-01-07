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
    public class CompetitionsViewModel : ViewModelBase
    {
        private IEnumerable<CompetitionViewModel> _competitions;
        private readonly IGameService _gameService;
        private readonly Func<Competition, CompetitionViewModel> _competitionViewModelFactory;
        private readonly IDialogProvider _dialogProvider;

        public CompetitionsViewModel(
            IGameService gameService,
            Func<Competition, CompetitionViewModel> competitionViewModelFactory,
            IDialogProvider dialogProvider)
        {
            this._dialogProvider = dialogProvider;
            _competitionViewModelFactory = competitionViewModelFactory;
            _gameService = gameService;
            Title = "Teams";
            SetCompetitions();
        }

        public IEnumerable<CompetitionViewModel> Competitions
        {
            get  { return _competitions; }
            set  { SetProperty(ref _competitions, value); }
        }

        private async void SetCompetitions()
        {
            try
            {
                IsBusy = true;
                var competitions = await _gameService.GetCompetitions();

                if (competitions == null)
                    return;

                Competitions = competitions
                    .Select(competition =>  _competitionViewModelFactory(competition))
                    .ToList();
            }
            catch (Exception ex)
            {
                Action action = async () =>
                {
                    var result = await _dialogProvider.DisplayActionSheet(ex.Message, "Cancel", null, "Retry");

                    if (result == "Retry")
                        SetCompetitions();
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
