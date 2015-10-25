using AlcmariaVictrix.Shared.Models;
using AlcmariaVictrix.Shared.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMolen.Mobile.Core.Helpers;
using WebMolen.Mobile.Core.Interfaces;
using WebMolen.Mobile.Core.ViewModels;
using Acr.UserDialogs;

namespace AlcmariaVictrix.Shared.ViewModels
{
    public class CompetitionsViewModel : ViewModelBase
    {
        private IEnumerable<CompetitionViewModel> _competitions;
        private readonly IGameService _gameService;
        private readonly Func<Competition, CompetitionViewModel> _competitionViewModelFactory;
        private readonly IUserDialogs _dialogService;
        private ObservableCollection<Grouping<string, CompetitionViewModel>> _competitionsGrouped;
        public ObservableCollection<Grouping<string, CompetitionViewModel>> CompetitionsGrouped
        {
            get  { return _competitionsGrouped; }
            set { SetProperty(ref _competitionsGrouped, value); }
        }

        public CompetitionsViewModel(
            IGameService gameService,
            Func<Competition, CompetitionViewModel> competitionViewModelFactory)
        {
            this._dialogService = UserDialogs.Instance;
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
                //Use linq to sorty our monkeys by name and then group them by the new name sort property
                var sorted = from monkey in Competitions
                             orderby monkey.Name
                             group monkey by monkey.NameSort into monkeyGroup
                             select new Grouping<string, CompetitionViewModel>(monkeyGroup.Key, monkeyGroup);

                //create a new collection of groups
                CompetitionsGrouped = new ObservableCollection<Grouping<string, CompetitionViewModel>>(sorted);
            }
            catch (Exception ex)
            {
                Action action = async () =>
                {
                    var r = await this._dialogService.ConfirmAsync("Reload competitions", "Can't load competitions", "Yes", "No");

                    if (r)
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
