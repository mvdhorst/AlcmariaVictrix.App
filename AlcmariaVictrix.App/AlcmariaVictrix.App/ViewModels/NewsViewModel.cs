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
using Acr.XamForms.UserDialogs;
using Xamarin.Forms;

namespace AlcmariaVictrix.Shared.ViewModels
{
    class NewsViewModel : ViewModelBase
    {
        private IEnumerable<GameViewModel> _games;
        private ObservableCollection<Grouping<DateTime, GameViewModel>> _gamesGrouped;
        private readonly IGameService _gameService;
        private readonly Func<Game, GameViewModel> _gameModelFactory;
        private readonly IDialogProvider _dialogProvider;
        private readonly IUserDialogService dialogService;


        public NewsViewModel(
            IGameService gameService,
            Func<Game, GameViewModel> gameViewModelFactory,
            IDialogProvider dialogProvider,
            IUserDialogService dialogService)
        {
            this.dialogService = dialogService;
            this._dialogProvider = dialogProvider;
            _gameModelFactory = gameViewModelFactory;
            _gameService = gameService;
            Title = "Nieuws";
            //SetGames();
        }

        public IEnumerable<GameViewModel> Games
        {
            get  { return _games; }
            set  { SetProperty(ref _games, value); }
        }
        public ObservableCollection<Grouping<DateTime, GameViewModel>> GamesGrouped
        {
            get { return _gamesGrouped; }
            set { SetProperty(ref _gamesGrouped, value); }
        }

        private async void SetGames()
        {
            Action action1 = async () =>
            {
                var r = await this.dialogService.ConfirmAsync("Pick a choice", "Pick Title", "Yes", "No");
                //var text = (r ? "Yes" : "No");
                //this.Result = "Confirmation Choice: " + text;

                //var result = await dialogService.ActionSheet(DisplayActionSheet("test", "Cancel", null, "Retry");

                if (r)
                    SetGames();
            };

            action1();
            IUserDialogService test = DependencyService.Get<IUserDialogService>();
            try
            {
                IsBusy = true;
                var games = await _gameService.GetGames().ConfigureAwait(false);

                if (games == null)
                    return;

                Games = games
                    .Select(game =>  _gameModelFactory(game))
                    .ToList();
                //Use linq to sorty our monkeys by name and then group them by the new name sort property
                var sorted = (from game in Games
                             orderby game.Game.GameDate
                             group game by game.Game.DateSort into gameGroup
                             select new Grouping<DateTime, GameViewModel>(gameGroup.Key, gameGroup.GroupBy(g => g.Game.Competition.Competition_id).SelectMany(g => g)));

                //create a new collection of groups
                GamesGrouped = new ObservableCollection<Grouping<DateTime, GameViewModel>>(sorted);
            }
            catch (Exception ex)
            {
                Action action = async () =>
                {
                    var r = await this.dialogService.ConfirmAsync("Pick a choice", "Pick Title", "Yes", "No");
                    //var result = await _dialogProvider.DisplayActionSheet(ex.Message, "Cancel", null, "Retry");

                    if (r)
                        SetGames();
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
