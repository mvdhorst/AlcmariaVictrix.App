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
        private IEnumerable<NewsItemViewModel> _feedItems;
        private IEnumerable<FeedItem> _newsItems;
        private readonly IGameService _gameService;
        private readonly Func<FeedItem, NewsItemViewModel> _feedItemModelFactory;
        private readonly IDialogProvider _dialogProvider;
        private readonly IUserDialogService dialogService;


        public NewsViewModel(
            IGameService gameService,
            Func<FeedItem, NewsItemViewModel> feedItemModelFactory,
            IDialogProvider dialogProvider,
            IUserDialogService dialogService)
        {
            this.dialogService = dialogService;
            this._dialogProvider = dialogProvider;
            _feedItemModelFactory = feedItemModelFactory;
            _gameService = gameService;
            Title = "Nieuws";
            SetNewsItems();
        }

        //public IEnumerable<NewsItemViewModel> NewsItems
        //{
        //    get { return _feedItems; }
        //    set { SetProperty(ref _feedItems, value); }
        //}

        public IEnumerable<NewsItemViewModel> NewsItems
        {
            get { return _feedItems; }
            set { SetProperty(ref _feedItems, value); }
        }

        private async void SetNewsItems()
        {
            IUserDialogService test = DependencyService.Get<IUserDialogService>();
            try
            {
                IsBusy = true;
                var newsItems = await _gameService.GetNewsItems().ConfigureAwait(false);

                if (newsItems == null)
                    return;
                NewsItems = newsItems.Select(n => _feedItemModelFactory(n))
               // NewsItems = newsItems
                    .ToList();           
            }
            catch (Exception ex)
            {
                Action action = async () =>
                {
                    var r = await this.dialogService.ConfirmAsync("Pick a choice", "Pick Title", "Yes", "No");
                    //var result = await _dialogProvider.DisplayActionSheet(ex.Message, "Cancel", null, "Retry");

                    if (r)
                        SetNewsItems();
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
